using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Mt
    {
        public const int W = 32;
        public const int N = 624;
        public const int M = 397;
        public const int R = 31;
        public const uint A = 0x9908B0DF;
        public const int U = 11;
        public const uint D = 0xFFFFFFFF;
        public const int S = 7;
        public const uint B = 0x9D2C5680;
        public const int T = 15;
        public const uint C = 0xEFC60000;
        public const int L = 18;

        public long Lower_Mask { get; private set; }
        public long Upper_Mask { get; private set; }

        private uint index;

        private ulong[] mt = new ulong[N];

        public Mt(ulong seed)
        {
            unchecked
            {
                Lower_Mask = (1 << R) - 1;
                Upper_Mask = (1 << W) - (1 << R);
            }
            Init(seed);
        }

        public void Init(ulong seed)
        {
            mt[0] = seed;
            for (index = 1; index < N; index++)
            {
                mt[index] = 1812433253U * (mt[index - 1] ^ (mt[index - 1] >> (W - 2))) + index;
            }
        }

        public ulong ExtractNumber()
        {
            if (index >= N)
            {
                for (int i = 0; i < N; i++)
                {
                    ulong x = ((mt[i] & (ulong)Upper_Mask) | (mt[(i + 1) % N] & (ulong)Lower_Mask));
                    ulong xA = x >> 1;
                    if ((x % 2) != 0)
                    {
                        xA = xA ^ A;
                    }
                    mt[i] = mt[(i + M) % N] ^ xA;
                }
                index = 0;
            }
            ulong y = mt[index];
            y ^= (y >> U);
            y ^= (y << S) & B;
            y ^= (y << T) & C;
            y ^= (y >> L);
            index++;
            return y;
        }
    }
}
