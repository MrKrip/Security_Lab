using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Lab3
    {
        public async Task<LcgCracker> LcgHack()
        {
            Client client = new Client();
            long[] temp = new long[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = await client.Play(1234567,1,1,"Lcg");
            }
            long a1 = temp[0] - temp[1];
            bool check = false;
            if (a1 < 0)
            {
                a1 = -a1;
                check = true;
            }

            long inverse_a1 = ModInverse(a1, Lcg.m);
            long a2 = temp[1] - temp[2];
            long a = inverse_a1 * a2 % Lcg.m;
            if(check)
            {
                a = -a;
            }

            Console.WriteLine("A :" + a);

            long c = (temp[1] - temp[0] * a) % Lcg.m;

            Console.WriteLine("C : " + c);

            LcgCracker cracked = new LcgCracker(a, c, temp[2]);
            var Bet = await client.Play(1234567, 1, cracked.Next(), "Lcg");
            return cracked;
        }

        private long ModInverse(long n, long m)
        {
            if (m == 1) return 0;
            long m0 = m;
            long x = 1L;
            long y = 0L;

            while (n > 1)
            {
                var q = n / m;

                (n, m) = (m, n % m);
                (x, y) = (y, x - q * y);
            }

            return x < 0 ? x + m0 : x;

        }


        public MtCracker MtHack()
        {
            Mt Generator = new Mt((ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            List<ulong> History = new List<ulong>();
            for (int i = 0; i < 624; i++)
            {
                History.Add(Generator.ExtractNumber());
            }
            List<ulong> used = new List<ulong>();
            for (int i = 0; i < Mt.N; i++)
            {
                var y = History[i];
                y ^= (y >> Mt.L);
                y ^= (y << Mt.T) & Mt.C;
                y ^= (y << Mt.S) & Mt.B;
                y ^= (y >> Mt.U);
                used.Add(y);
            }
            int kek = 0;
            for (int i = 0; i < Mt.N; i++)
            {
                if (used[i] == Generator.mt[i])
                    kek++;
            }
            Console.WriteLine(kek);
            MtCracker cracked = new MtCracker(used.ToArray(), 624);

            Console.WriteLine($"Cracked : {cracked.Next()}");
            Console.WriteLine($"Generator : {Generator.ExtractNumber()}");
            return cracked;
        }
    }
}
