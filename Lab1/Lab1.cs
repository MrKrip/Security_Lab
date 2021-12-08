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
            var base64EncodedBytes = Convert.FromBase64String(Context);
            var Text = Encoding.UTF8.GetString(base64EncodedBytes);
            // IndexOfCoincidence(Text);      
             int KeyLength = 3;
             var MaxIterator = Math.Pow(255, KeyLength);
             for (int i = 0; i <= MaxIterator; i++)
             {
                 byte[] Key = new byte[KeyLength];
                 for (int j = 0; j < KeyLength; j++)
                 {
                     Key[j] = GenerateByteForKey(i, KeyLength - (j + 1));
                 }

                 byte[] output = new byte[Text.Length];
                 for (int c = 0; c < Text.Length; c++)
                 {
                     output[c] = (byte)(Text[c] ^ Key[c % KeyLength]);
                 }

                 string dexored = Encoding.ASCII.GetString(output);
                 if (dexored.Contains(" the "))
                 {
                     Console.WriteLine("-------------------------------------------------");
                     Console.Write("Key => ");
                     for(int j=0;j<KeyLength;j++)
                     {
                         Console.Write($"{(char)Key[j]}");
                     }
                     Console.WriteLine();
                     Console.WriteLine(dexored);
            
                 }
         
        }
            
        }

        public void Lab1_3(string Context) {
            Genetic genAlgorithm = new Genetic(Context);
            genAlgorithm.getTrigram();
            string answer = genAlgorithm.GeneticDecrypt();
            Console.WriteLine(answer);
        }
        internal void lab_4(string context)
        {
            IndexOfCoincidence(context);
        }





        private void IndexOfCoincidence(string Text)
        {
            string temp = Text;
            var ListKeys = new List<int>();
            var ListCoinc = new List<double>();
            var BetterKey = new List<int>();
            double CoincidenceCount;
            for (int i = 0; i < Text.Length; i++)
            {
                temp = temp.Last() + temp.Substring(0, temp.Length - 1);
                CoincidenceCount = 0;
                for (int j = 0; j < Text.Length; j++)
                {
                    if (Text[j] == temp[j])
                    {
                        CoincidenceCount++;
                    }
                }               
                ListKeys.Add(i);
                ListCoinc.Add(CoincidenceCount / temp.Length);
               //Console.WriteLine($"{ListKeys[i] + 1} => {ListCoinc[i]}");
            }
                  
            for (var j = 1; j < ListCoinc.Count; j++)
            {               
                if (ListCoinc[j] >= 0.06)
                {
                    BetterKey.Add(ListKeys[j]);
                }                
            }
            for (var j = 0; j < BetterKey.Count; j++)
            {
                Console.WriteLine($"Key:{ BetterKey[j]+1}");
            }            
        }

        private byte GenerateByteForKey(int iterator, int position)
        {

            return Convert.ToByte((int)((iterator / Math.Pow(255, position)) % 255));

        }

    }
}
