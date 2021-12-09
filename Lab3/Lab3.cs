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
        public LcgCracker LcgHack()
        {
            Lcg lcg = new Lcg(1103515245, 12345, 54321);//1103515245
            BigInteger[] temp = new BigInteger[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = lcg.Next();
            }
            BigInteger a1 = temp[0] - temp[1];
            if (a1 < 0)
            {
                a1 = -a1;
            }

            BigInteger inverse_a1 = ModInverse(a1, Lcg.m);
            BigInteger a2 = temp[1] - temp[2];
            if (a2 < 0)
                a2 = -a2;
            BigInteger a_first = inverse_a1 * (a2 % Lcg.m) % Lcg.m;
            BigInteger a_second = a_first;
            if (a_second < 0)
                a_second = -a_second;
            a_second = Lcg.m - a_second;

            Console.WriteLine("First a " + a_first);
            Console.WriteLine("Second a " + a_second);

            if (temp[2] - temp[1] * a_first < 0)
                a_first = -a_first;
            if (temp[2] - temp[1] * a_second < 0)
                a_second = -a_second;
            var a = temp[2] - temp[1] * a_first;
            BigInteger c_first = (temp[2] - temp[1] * a_first) % Lcg.m;
            BigInteger c_second = (temp[2] - temp[1] * a_second) % Lcg.m;

            Console.WriteLine("First c " + c_first);
            Console.WriteLine("Second c " + c_second);

            BigInteger next1 = (a_first * temp[2] + c_second) % Lcg.m;
            BigInteger next2 = (a_second * temp[2] + c_first) % Lcg.m;
            if (next1 < 0)
                next1 = -next1;
            if (next2 < 0)
                next2 = -next2;
            Console.WriteLine(new string('-', 30));

            Console.WriteLine("First next " + next1);
            Console.WriteLine("Second next " + next2);
            Console.WriteLine(new string('-', 30));

            BigInteger next = lcg.Next();
            Console.WriteLine("Next rand " + next);
            LcgCracker cracked;
            if (next == next1)
            {
                cracked = new LcgCracker(a_first, c_second, next);
            }
            else
            {
                cracked = new LcgCracker(a_second, c_first, next);
            }
            return cracked;
        }

        private BigInteger ModInverse(BigInteger n, BigInteger m)
        {
            if (m == 1) return 0;
            BigInteger m0 = m;
            BigInteger x = 1;
            BigInteger y = 0;

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
