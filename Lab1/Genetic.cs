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
        static private char[] CryptoLetters=
       {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 
            'H', 'I', 'J', 'K', 'L', 'M', 'N', 
            'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
        };
        private int lettersCount = 26;
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
            double sum = 0;
            var count = string.Empty;
            var threeLetter = string.Empty;
            double value = 0;
            foreach (var line in text)
            {
                int border = line.Length;
                count = line[4..border];
                value = Convert.ToDouble(count);
                sum += value;
            }
                foreach (var line in text)
            {
                threeLetter = line[0..3];
                int border = line.Length;
                count = line[4..border];                
                value = Convert.ToDouble(count)/sum;
                sum += value;
                trigramDictionary.Add(threeLetter.ToUpper(), value);
            }
        }

        public void GeneticDecrypt() {
            int generation = 1;
            var chromosomes = GenerateStartPopulation(1000);
            while (generation < 100) {
                chromosomes = OneHundredBestChromosomes(chromosomes);
                generation++;
            }

        }

        private List<char[]> OneHundredBestChromosomes(List<char[]> chromosomes)
        {
            double[] dio = new double[1000];
            double[] dio2 = new double[1000];
            List<char[]> chromosomes2 = chromosomes;

            for (int i=0; i < chromosomes.Count; i++) {
                dio[i] = FitnesFunction(chromosomes[i]);
              
            }
            Sorttop(ref dio, ref chromosomes2);
            chromosomes2.RemoveRange(100, 900);

            return chromosomes.OrderByDescending(FitnesFunction).Take(100).ToList();
        }

        private void Sorttop(ref double[] dio, ref List<char[]> chromosomes)
        {
            bool sorted;
            for (int i = 0; i < dio.Length - 1; i++)
            {
                sorted = true;
                for (int j = dio.Length - 1; j > i; j--)
                {
                   
                    if (dio[j] < dio[j - 1])
                    {
                        double temp = dio[j - 1];
                        char[] tempChar = chromosomes[j - 1];
                        dio[j - 1] = dio[j];
                        chromosomes[j - 1] = chromosomes[j];
                        dio[j] = temp;
                        chromosomes[j] = tempChar;
                        sorted = false;
                    }
                }
                if (sorted)
                    break;
            }           
           Array.Reverse(dio);
            chromosomes.Reverse();
        }
     

        private double FitnesFunction(char[] chromosome)
        {
            var decryptionOption = SubstitutionCipher(CharContext, chromosome);
            double percent = 0;
            for (int i = 0; i < decryptionOption.Length - 2; i++)
            {
                var threeLetters = decryptionOption.Substring(i, 3);
                if (trigramDictionary.ContainsKey(threeLetters))                {
                    percent += trigramDictionary[threeLetters];
                }
            }
            var percentSimilarity = percent / (decryptionOption.Length - 2);

            return percentSimilarity;
        }

      

        private string SubstitutionCipher(char[] StrangeText, char[] chromosome)
        {
            var TempDecript = CharDecrypt;
            for (int i = 0; i < StrangeText.Length; i++)
            {
                var letter = StrangeText[i];
                string chromosomeStr = new string(chromosome);
                var indexOfChange = chromosomeStr.IndexOf(letter);
                TempDecript[i] = CryptoLetters[indexOfChange];                
            }
            return new string(TempDecript);
        }

        private List<char[]> GenerateStartPopulation(int numberOfStart)
        {
            var ChromosomeSet = new List<char[]>();          
           
            Random random = new Random();
            for (int i = 0; i < numberOfStart; i++)
            {
                List<char> tempLetterList = CryptoLetters.ToList();
                var chromosome = new char[lettersCount];
                for (int j = 0; j < CryptoLetters.Length; j++)
                {
                    var RandLetter = tempLetterList[random.Next(0, tempLetterList.Count)];
                    chromosome[j] = RandLetter;
                    tempLetterList.Remove(RandLetter);
                }
                ChromosomeSet.Add(chromosome);

            }
            return ChromosomeSet;
        }
    }
}
