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
        public void LcgHack()
        {
            Lcg lcg = new Lcg(1103515245, 123, 54321);//1103515245
            BigInteger[] temp = new BigInteger[3];
            for (int i = 0; i < 3; i++)
            {
                temp[i] = lcg.Next();
            }
            BigInteger a1 = temp[0] - temp[1];
            if (a1 < 0)
                a1 = -a1;
            BigInteger inverse_a1 = ModInverse(a1, Lcg.m);
            BigInteger a2 = temp[1] - temp[2];
            if (a2 < 0)
                a2 = -a2;
            BigInteger a = inverse_a1 * (a2 % Lcg.m) % Lcg.m;
            a=Lcg.m-a;
            Console.WriteLine(a);
            BigInteger c = (temp[2] - temp[1] * a) % Lcg.m;
            Console.WriteLine(Lcg.m+c);
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
    }
}
