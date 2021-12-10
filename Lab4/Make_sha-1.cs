using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;


namespace Lab4
{
    class Make_sha_1
    {
     
        public static string GetHash(string str )
        {
            byte[] salt = GenerateSaltBites();
            SHA1 sha1Hash = SHA1.Create();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(str);       
           
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] passwordWithSaltBytes =new byte[sourceBytes.Length + salt.Length];

            for (int i = 0; i < sourceBytes.Length; i++)
            {
                passwordWithSaltBytes[i] = sourceBytes[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                passwordWithSaltBytes[sourceBytes.Length + i] = salt[i];

            }          
            return BitConverter.ToString(passwordWithSaltBytes).Replace("-", string.Empty);
        }

        static private byte[] GenerateSaltBites()
        {
            var length = new byte[12];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(length);
            return length;
        }


    }
}
