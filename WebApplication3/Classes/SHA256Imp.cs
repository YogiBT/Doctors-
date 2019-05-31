

//namespace WebApplication3.Classes
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Classes
{
    class SHA256Imp
    {
        // ostanki kubičnega korena prvih 64 praštevil
         uint[] constantsK = new uint[64] {
            0x428A2F98, 0x71374491, 0xB5C0FBCF, 0xE9B5DBA5, 0x3956C25B, 0x59F111F1, 0x923F82A4, 0xAB1C5ED5,
            0xD807AA98, 0x12835B01, 0x243185BE, 0x550C7DC3, 0x72BE5D74, 0x80DEB1FE, 0x9BDC06A7, 0xC19BF174,
            0xE49B69C1, 0xEFBE4786, 0x0FC19DC6, 0x240CA1CC, 0x2DE92C6F, 0x4A7484AA, 0x5CB0A9DC, 0x76F988DA,
            0x983E5152, 0xA831C66D, 0xB00327C8, 0xBF597FC7, 0xC6E00BF3, 0xD5A79147, 0x06CA6351, 0x14292967,
            0x27B70A85, 0x2E1B2138, 0x4D2C6DFC, 0x53380D13, 0x650A7354, 0x766A0ABB, 0x81C2C92E, 0x92722C85,
            0xA2BFE8A1, 0xA81A664B, 0xC24B8B70, 0xC76C51A3, 0xD192E819, 0xD6990624, 0xF40E3585, 0x106AA070,
            0x19A4C116, 0x1E376C08, 0x2748774C, 0x34B0BCB5, 0x391C0CB3, 0x4ED8AA4A, 0x5B9CCA4F, 0x682E6FF3,
            0x748F82EE, 0x78A5636F, 0x84C87814, 0x8CC70208, 0x90BEFFFA, 0xA4506CEB, 0xBEF9A3F7, 0xC67178F2
        };

        //inital hash value
        // prvih 32 bitov od delov ostanka od korena od prvih osem praštevil (hash value)
        uint[] initalHashValue = new uint[8] {
            0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19
        };

        byte[] pending_block = new byte[64];
        uint pending_block_offset = 0;
        uint[] buffer = new uint[16];

        public  uint bits_processed = 0;

        bool closed = false;
        // main function
        public List<byte> Hash(string password)
        {
            SHA256Imp sha256 = new SHA256Imp();
            byte[] buf = new byte[8196];
            
            uint bytes_read;
            do
            {
                //bytes_read = (uint)stream.Read(buf, 0, buf.Length);
                
                bytes_read = (uint)password.Length;
                if (bytes_read == 0)
                {
                    break;
                }
                else
                {
                    byte[] ba = Encoding.Default.GetBytes(password);
                    for(int i = 0; i < ba.Length; i++)
                    {
                        buf[i] = ba[i];


                    }
                   /* buf[0] = 12;
                    buf[1] = 0x5;*/

                }

                sha256.ProcessBytes(buf, 0, bytes_read);
            }
            while (bytes_read == 8196);

            return sha256.GetHash();
        }
        // procesira block po block (64 bytes)
        public void ProcessBytes(byte[] data, uint offset, uint len)
        {
            bits_processed += len * 8;

            while (len > 0)
            {
                uint amount_to_copy = 0;

                if (len < 64)
                {
                    if (pending_block_offset + len > 64)
                    {
                        amount_to_copy = 64 - pending_block_offset;
                    }
                    else
                    {
                        amount_to_copy = len;
                    }
                }
                else
                {
                    amount_to_copy = 64 - pending_block_offset;
                }

                Array.Copy(data, offset, pending_block, pending_block_offset, amount_to_copy);
                len -= amount_to_copy;
                offset += amount_to_copy;
                pending_block_offset += amount_to_copy;

                if (pending_block_offset == 64)
                {

                    toUintArray(pending_block, buffer);
                    processBlock(buffer);
                    pending_block_offset = 0;
                }
            }
        }
        // procesira posamezni block, hashiranje
        void processBlock(uint[] M)
        {
            // 1. pripravi message schedule
            uint[] W = new uint[64];
            for (int t = 0; t < 16; ++t)
            {
                W[t] = M[t];
            }

            for (int t = 16; t < 64; ++t)
            {
                W[t] = SmallSigma1(W[t - 2]) + W[t - 7] + SmallSigma0(W[t - 15]) + W[t - 16];
            }

            // 2. inicializacija delovne spremenljivke (8)
            uint a = initalHashValue[0];
            uint b = initalHashValue[1];
            uint c = initalHashValue[2];
            uint d = initalHashValue[3];
            uint e = initalHashValue[4];
            uint f = initalHashValue[5];
            uint g = initalHashValue[6];
            uint h = initalHashValue[7];

            // 3. izračunaj hashe
            for (int t = 0; t < 64; ++t)
            {
                uint T1 = h + BigSigma1(e) + Ch(e, f, g) + constantsK[t] + W[t];
                uint T2 = BigSigma0(a) + Maj(a, b, c);
                h = g;
                g = f;
                f = e;
                e = d + T1;
                d = c;
                c = b;
                b = a;
                a = T1 + T2;
            }
            // 4. Dodaj hashe trenutnim hashem
            initalHashValue[0] = a + initalHashValue[0];
            initalHashValue[1] = b + initalHashValue[1];
            initalHashValue[2] = c + initalHashValue[2];
            initalHashValue[3] = d + initalHashValue[3];
            initalHashValue[4] = e + initalHashValue[4];
            initalHashValue[5] = f + initalHashValue[5];
            initalHashValue[6] = g + initalHashValue[6];
            initalHashValue[7] = h + initalHashValue[7];
        }


        // vrne byte array hasha
        public List<byte> GetHash()
        {
            return toByteArray(GetHashUInt32());
        }
        // padding
        public List<uint> GetHashUInt32()
        {
            if (!closed)
            {
                uint size_temp = bits_processed;

                ProcessBytes(new byte[1] { 0x80 }, 0, 1);

                uint available_space = 64 - pending_block_offset;

                if (available_space < 8)
                {
                    available_space += 64;
                }


                byte[] padding = new byte[available_space];

                //padding tako da je block multiple of 512
                for (uint i = 1; i <= 8; ++i)
                {
                    padding[padding.Length - i] = (byte)size_temp;
                    size_temp >>= 8;
                }
                // 0u insigned intiger
                ProcessBytes(padding, 0u, (uint)padding.Length);

                closed = true;
            }

            return initalHashValue.ToList();
        }
        // byte v uint
         void toUintArray(byte[] src, uint[] dest)
        {
            for (uint i = 0, j = 0; i < dest.Length; ++i, j += 4)
            {
                dest[i] = ((uint)src[j + 0] << 24) | ((uint)src[j + 1] << 16) | ((uint)src[j + 2] << 8) | ((uint)src[j + 3]);
            }
        }
        // uint v byte array
         List<byte> toByteArray(List<uint> src)
        {
            byte[] dest = new byte[src.Count * 4];
            int pos = 0;

            for (int i = 0; i < src.Count; i++)
            {
                // 24 - 31
                dest[pos++] = (byte)(src[i] >> 24);
                // 16 - 23
                dest[pos++] = (byte)(src[i] >> 16);
                // 8 - 15
                dest[pos++] = (byte)(src[i] >> 8);
                // 0 - 7
                dest[pos++] = (byte)(src[i]);
            }

            return dest.ToList();
        }

         uint RotateLeft(uint x, byte n)
        {
            return (x << n) | (x >> (32 - n));
        }

         uint RotateRight(uint x, byte n)
        {
            //32 - n zato ker uporablja sha256 32bitne besede
            return (x >> n) | (x << (32 - n));
        }
        // choice function
         uint Ch(uint i, uint j, uint k)
        {
            return (i & j) ^ ((~i) & k);
        }
        // majority function
         uint Maj(uint i, uint j, uint k)
        {
            return (i & j) ^ (i & k) ^ (j & k);
        }

        // velika sigma 0
         uint BigSigma0(uint x)
        {
            return RotateRight(x, 2) ^ RotateRight(x, 13) ^ RotateRight(x, 22);
        }
        //velika sigma 1
         uint BigSigma1(uint x)
        {
            return RotateRight(x, 6) ^ RotateRight(x, 11) ^ RotateRight(x, 25);
        }
        //mala sigma 0
         uint SmallSigma0(uint x)
        {
            return RotateRight(x, 7) ^ RotateRight(x, 18) ^ (x >> 3);
        }
        //mala sigma 1
         uint SmallSigma1(uint x)
        {
            return RotateRight(x, 17) ^ RotateRight(x, 19) ^ (x >> 10);
        }
        public bool ValidatePassword(string password, string dbHash)
        {


            /*
             
            byte[] byteArray = Encoding.ASCII.GetBytes(password);
            MemoryStream stream = new MemoryStream(byteArray);
            List<byte> temp = SHA256Imp.Hash(stream);
            
            string newHash = ArrayToString(temp);

             */
           /* byte[] byteArray = Encoding.ASCII.GetBytes(password);
            MemoryStream stream = new MemoryStream(byteArray);*/
            List<byte> hashToValidate = Hash(password);
            string newHash = ArrayToString(hashToValidate);
            int verify = 0;
            verify = string.Compare(newHash, dbHash);
            if (verify == 0)
            {
                return true;
            }
            return false;

        }
        public string ArrayToString(List<byte> arr)
        {
            StringBuilder s = new StringBuilder(arr.Count * 2);
            for (int i = 0; i < arr.Count; ++i)
            {
                s.AppendFormat("{0:x}", arr[i]);
            }

            return s.ToString();
        }
    }
}