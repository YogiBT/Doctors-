using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebApplication3.Classes;
namespace WebApplication3.Classes
{
    /// <summary>
    /// password encryption
    /// </summary>
    public class Encryption
    {
        public const int SALT_SIZE = 24;
        public const int HASH_SIZE = 24;
        public const int PBKDF2_ITT = 500;
        /// <summary>
        /// get password as an input , building a random salt, using the password and the salt creates a hash .
        /// </summary>
        /// <param name="password"></param>
        /// <returns>string converted</returns>
        public string CreateHash(string password)
        {
            string newHash;
            string newHash2;
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_SIZE]; // random salt 
            csprng.GetBytes(salt);

            //  byte[] byteArray = Encoding.ASCII.GetBytes(password);
            // MemoryStream stream = new MemoryStream(byteArray);
            // MemoryStream s1 = GenerateStreamFromString(password); 
            /* using (var stream = GenerateStreamFromString(password))
             {

                 List<byte> temp = SHA256Imp.Hash(stream);

                 newHash = ArrayToString(temp);


                 Clear(stream);
             }*/
            //s1.Close();
            //s1.Dispose();



            //byte[] salt = Convert.(newHash);
            SHA256Imp hasher1 = new SHA256Imp();
            
            List<byte> temp = hasher1.Hash(password);

            newHash = ArrayToString(temp);
          /*  SHA256Imp hasher2 = new SHA256Imp();
            List<byte> temp2 = hasher2.Hash(password);

            newHash2 = ArrayToString(temp2);*/

            byte[] hash = PBKDF2(password, salt, PBKDF2_ITT,HASH_SIZE);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash) + ":" + newHash;
        }



        /// <summary>
        /// returns the hash function        
        /// /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="pBKDF2_ITT"></param>
        /// <param name="outputBytes"></param>
        /// <returns>byte</returns>
        private byte[] PBKDF2(string password, byte[] salt, int pBKDF2_ITT, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);  //hash function USING salt and the password
            pbkdf2.IterationCount = pBKDF2_ITT;
            return pbkdf2.GetBytes(outputBytes);
        }
        /// <summary>
        /// checks if the password is the same char by char.
        /// </summary>
        /// <param name="dbHash"></param>
        /// <param name="passHash"></param>
        /// <returns>bool diff</returns>
        private bool SlowEquals(byte [] dbHash , byte [] passHash)
        {
            uint diff = (uint)dbHash.Length ^ (uint)passHash.Length; // XOR opporation

            for (int i = 0; i < dbHash.Length && i < passHash.Length; i++) // check char by char
                diff = (uint)dbHash[i] ^ (uint)passHash[i];

            return diff == 0;

        }
        /// <summary>
        /// validate the password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="dbHash"></param>
        /// <returns>bool slow hash</returns>
        public bool ValidatePassword(string password , string dbHash)
        {
            char[] delimiter = { ':' };
            string[] split = dbHash.Split(delimiter);
            byte[] salt = Convert.FromBase64String(split[0]);
            byte[] hash = Convert.FromBase64String(split[1]); 
            string sha256 = split[2];
           /* byte[] byteArray = Encoding.ASCII.GetBytes(password);
            MemoryStream stream = new MemoryStream(byteArray);
            List<byte> temp = SHA256Imp.Hash(stream);

            string newHash = ArrayToString(temp);*/
            SHA256Imp s = new SHA256Imp();
            //s.ValidatePassword(password, salt2);
       
            byte[] hashToValidate = PBKDF2(password, salt, PBKDF2_ITT, hash.Length);
            if (SlowEquals(hash, hashToValidate) == true && s.ValidatePassword(password, sha256) == true)
                return true;
            else
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
        public static MemoryStream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            
            
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            stream.SetLength(0);

            return stream;
        }
        public static void Clear(MemoryStream source)
        {
            byte[] buffer = source.GetBuffer();
            Array.Clear(buffer, 0, buffer.Length);
            source.Position = 0;
            source.SetLength(0);
        }
    }
}