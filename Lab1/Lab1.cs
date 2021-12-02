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

        public void Lab1_2(string Context)
        {
            Regex regex = new Regex(@"(^\w+) \w+");
            var base64EncodedBytes = Convert.FromBase64String(Context);
            var Text = Encoding.UTF8.GetString(base64EncodedBytes);
            //IndexOfCoincidence(Text);
            int KeyLength = 3;
            var MaxIterator = Math.Pow(255, KeyLength);
            for (int i=0;i<=MaxIterator;i++)
            {
                //Console.WriteLine(i);
                string Key = string.Empty;
                for(int j=0;j<KeyLength;j++)
                {
                    Key += (char)GenerateByteForKey(i,KeyLength-(j+1));
                }

                byte[] output = new byte[Text.Length];
                for (int c = 0; c < Text.Length; c++)
                {
                    output[c] = (byte)(Text[c] ^ Key[c%KeyLength]);
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


        public void IndexOfCoincidence(string Text)
        {
            string temp = Text;
            for (int i = 0; i < Text.Length; i++)
            {
                temp = temp.Last() + temp.Substring(0, temp.Length - 1);
                double CoincidenceCount = 0;
                for (int j = 0; j < Text.Length; j++)
                {
                    if (Text[j] == temp[j])
                    {
                        CoincidenceCount++;
                    }
                }
                Console.WriteLine($"{i + 1} => {CoincidenceCount / temp.Length}");
            }
        }

        public byte GenerateByteForKey(int iterator,int position)
        {
            try
            {
                return Convert.ToByte((int)(iterator / (Math.Pow(255, position))));
            }
            catch(OverflowException)
            {
                return Convert.ToByte((int)(iterator % 255));
            }            
        }
    }
}
