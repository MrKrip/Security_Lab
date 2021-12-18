using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Helpers
{
    public class AES_GCM : IHelper
    {
        public string Encrypt(string plaintext, IConfiguration _config)
        {
            string output = string.Empty;
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Keys:AES-GCM"));
            AesGcm Aes = new AesGcm(key);
            return output;
        }
    }
}
