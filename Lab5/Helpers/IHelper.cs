using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Helpers
{
    public interface IHelper
    {
        public string Encrypt(string plaintext, IConfiguration _config, byte[] nonce, byte[] tag);

        public string Decrypt(string chipertext, IConfiguration _config, byte[] nonce, byte[] tag);
    }
}
