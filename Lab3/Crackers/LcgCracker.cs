using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class LcgCracker
    {
        private BigInteger a;
        private BigInteger c;
        public static readonly BigInteger m = 4294967296;
        private BigInteger _last;

        public LcgCracker(BigInteger a, BigInteger c, BigInteger last)
        {
            this.a = a;
            this.c = c;
            _last = last;
        }

        public BigInteger Next()
        {
            _last = (a * _last + c) % m;
            return _last;
        }
    }
}
