using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Genetic
    {
        private char[] CryptoLetters=
       {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 
            'H', 'I', 'J', 'K', 'L', 'M', 'N', 
            'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
        };
        private Dictionary<string, double> trigramDictionary = new Dictionary<string, double>();
        private char[] CharContext;
        private char[] CharDecrypt;

        public Genetic(String Context)
        {
            CharContext = Context.ToCharArray();
            CharDecrypt = new char[CharContext.Length];            
        }

        public void getTrigram() {
            var text = File.ReadAllLines(@"D:\security\Security_Lab_1\Lab1\trigram.txt");
            foreach (var line in text)
            {
                var threeLetter = line[0..3];
                int a = line.Length - 1;
                var percent = line[6..a];
                double value = Convert.ToDouble(percent) / 100;
                trigramDictionary.Add(threeLetter, value);
            }
        }

        public void GeneticDecrypt() {
        
        }

        }
}
