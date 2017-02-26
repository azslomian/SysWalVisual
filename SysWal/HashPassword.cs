using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SysWal
{
    class HashPassword
    {
        /*
        public static string ByteArrayToHexString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
        public String CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        public string HashUserPassword(string password)
        {
            string salt = CreateSalt(10);
            string hashedPassword = GenerateSHA256Hash(password, salt);
            return hashedPassword;
        }

        public string ComparePassword(string password, string salt)
        {
            string hashedPassword = GenerateSHA256Hash(password, salt);
            return hashedPassword;
        }
        */
        //////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////

            public static String sha256_hash(String value)
            {
                StringBuilder Sb = new StringBuilder();

                using (SHA256 hash = SHA256Managed.Create())
                {
                    Encoding enc = Encoding.UTF8;
                    Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                    foreach (Byte b in result)
                        Sb.Append(b.ToString("x2"));
                }

                return Sb.ToString();
            }
            

        public static string ByteArrayToHexString(byte[] ba)
        {
            return string.Join(string.Empty, ba.Select(x => x.ToString("x2")));
        }
        public string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            using(SHA256 sha256hashstring = SHA256.Create())
            {
                byte[] hash = sha256hashstring.ComputeHash(bytes);
                return ByteArrayToHexString(hash);
            }
           
        }

        public string HashUserPassword(string password, string pepper)
        {
            string hashedPassword = GenerateSHA256Hash(password, pepper);
            return hashedPassword;
        }

        public string ComparePassword(string password, string pepper)
        {
            string hashedPassword = GenerateSHA256Hash(password, pepper);
            return hashedPassword;
        }
    }
}
