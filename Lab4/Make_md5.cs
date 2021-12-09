using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lab4
{
    class Make_md5
    {
        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var ByteHash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            string hash = Convert.ToBase64String(ByteHash);

            return hash;
        }
    }
}
