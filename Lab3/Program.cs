using System;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Lab3 huinya = new Lab3();
            var Lcg = await huinya.LcgHack();
        }
    }
}
