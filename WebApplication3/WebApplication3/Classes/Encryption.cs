using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

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
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_SIZE]; // random salt 
            csprng.GetBytes(salt);


            byte[] hash = PBKDF2(password, salt, PBKDF2_ITT,HASH_SIZE);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
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

            byte[] hashToValidate = PBKDF2(password, salt, PBKDF2_ITT, hash.Length);

            return SlowEquals(hash, hashToValidate);
        }

    }
}