using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1
{
    class Lab1
    {
        public void Lab1_1(string Context)
        {
            Regex regex = new Regex(@"(^\w+) \w+");
            String ascii = "";

            for (int i = 0; i < Context.Length; i += 2)
            {

                String part = Context.Substring(i, 2);

                char ch = (char)Convert.ToInt32(part, 16); ;

                ascii = ascii + ch;
            }
            for (int i = 0; i < 255; i++)
            {

                byte[] output = new byte[ascii.Length];
                for (int c = 0; c < ascii.Length; c++)
                {
                    output[c] = (byte)(ascii[c] ^ Convert.ToByte(i));
                }

                string dexored = Encoding.ASCII.GetString(output);
                if (regex.IsMatch(dexored))
                {
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine(i);
                    Console.WriteLine();
                    Console.WriteLine(dexored);                    
                }


            }
        }
    }
}
