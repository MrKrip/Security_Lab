using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Lab4
{
    class Make_sha_1
    {
        public static string GetHash(string str)
        {
            SHA1 sha1Hash = SHA1.Create();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            //Console.WriteLine("The SHA1 hash of " + str + " is: " + hash);
            return hash;
        }

        
    }
}
