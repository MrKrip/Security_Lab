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

        public string Decrypt(string chipertext, IConfiguration _config, byte[] nonce, byte[] tag)
        {
            string output = string.Empty;
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Keys:AES-GCM"));
            AesGcm Aes = new AesGcm(key);
            var chipertextBytes = Convert.FromBase64String(chipertext);
            var plaintext = new byte[chipertextBytes.Length];
            Aes.Decrypt(nonce, chipertextBytes, tag, plaintext);
            output = Encoding.ASCII.GetString(plaintext);
            return output;

        }

        public string Encrypt(string plaintext, IConfiguration _config,byte[] nonce, byte[] tag)
        {
            string output = string.Empty;
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Keys:AES-GCM"));
            AesGcm Aes = new AesGcm(key);
            var plaintextBytes = Encoding.ASCII.GetBytes(plaintext);
            var ciphertext = new byte[plaintextBytes.Length];           
            Aes.Encrypt(nonce, plaintextBytes, ciphertext, tag);
            output = Convert.ToBase64String(ciphertext);
            return output;
        }
    }
}
