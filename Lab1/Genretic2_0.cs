using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.cipher
{
    class Genetic2_0 
    {
        static private char[] CryptoLetters =
      {
            'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
        };
        private int lettersCount = 26;
        private Dictionary<string, double> trigramDictionary = new Dictionary<string, double>();
        private char[] CharContext;
        private string CharDecrypt;
        private int mutationProbability = 80;
        private int KeyCount;

        public Genetic2_0(String Context, int keyCount)
        {
            CharContext = Context.ToCharArray();
            this.KeyCount = keyCount;
        }

        public void getTrigram()
        {
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
                value = Convert.ToDouble(count) / sum;
                sum += value;
                trigramDictionary.Add(threeLetter.ToUpper(), value);
            }
        }

        public void GeneticDecrypt()
        {
            int generation = 1;
            var chromosomes = GenerateStartPopulation(1000);
            double bestIndividual;
            List<char[]> bestish = new List<char[]>();
            while (generation < 300)
            {
                chromosomes = OneHundredBestChromosomes(chromosomes);
                bestish = chromosomes[0];
                bestIndividual = FitnesFunction(bestish);
                CharDecrypt = SubstitutionCipher(CharContext, bestish);
                Console.WriteLine($"\nOriginal:{ new string(CryptoLetters)}\nGeneration: {generation} - BestIndividual: {bestIndividual}");
                Crossover(ref chromosomes);
                Mutation(ref chromosomes);
                generation++;
            }
            //string a = SubstitutionCipher(CharContext, chromosomes[0]);
            // Console.WriteLine(a);
            foreach (var best in bestish) {
                Console.WriteLine($"\n{new string (best)}\n");
            }
            Console.WriteLine(CharDecrypt);

        }

        private void Mutation(ref List<List<char[]>> chromosomes)
        {

            foreach (var chromosome in chromosomes)
            {
                Random random = new Random();
                var percentMutation = random.Next(100);
                foreach (var piceofChromosome in chromosome)
                    if (percentMutation <= mutationProbability)
                    {
                        var mutateGen1 = RandGen();
                        var mutateGen2 = RandGen();
                        if (mutateGen1 == mutateGen2)
                        {
                            mutateGen2++;
                        }

                        (piceofChromosome[mutateGen1], piceofChromosome[mutateGen2]) = (piceofChromosome[mutateGen2], piceofChromosome[mutateGen1]);
                    }                
            }
        }
        private int RandGen()
        {
            Random random = new Random();          
            var mutateGen = random.Next(lettersCount-1);
            return mutateGen;
        }

        private void Crossover(ref List<List<char[]>> chromosomes)
        {
            Random random = new Random();

            var children = new List<List<char[]>>();
            
            for (var i = 0; i < chromosomes.Count; i++)
            {
                for (int j = 0; j < 50; j++) //special place
                {
                    var fourChlidren = new List<char[]>();
                    for (int l = 0; l < KeyCount; l++)
                    {
                        var Mother = chromosomes[i][l];
                        var Father = chromosomes[j][l];
                        var child = new char[Mother.Length];
                        var gens = CryptoLetters.ToList();
                        bool queue = true;
                        for (var k = 0; k < Mother.Length; k++)
                        {
                            var isContainMotherGen = child.Contains(Mother[k]);
                            var isContainFatherGen = child.Contains(Father[k]);

                            if (isContainMotherGen && isContainFatherGen)
                            {
                                var randomGen = gens[random.Next(0, gens.Count)];
                                child[k] = randomGen;
                                gens.Remove(randomGen);
                            }
                            else if (isContainMotherGen)
                            {
                                child[k] = Father[k];
                                gens.Remove(Father[k]);
                            }
                            else if (isContainFatherGen)
                            {
                                child[k] = Mother[k];
                                gens.Remove(Mother[k]);
                            }
                            else
                            {
                                if (queue)
                                {
                                    child[k] = Mother[k];
                                    gens.Remove(Mother[k]);
                                    queue = false;
                                }
                                else
                                {
                                    child[k] = Father[k];
                                    gens.Remove(Father[k]);
                                    queue = true;
                                }

                            }

                        }
                        fourChlidren.Add(child);
                    }
                    children.Add(fourChlidren);
                   
                }
            }
            chromosomes = children;
        }


        private List<List<char[]>> OneHundredBestChromosomes(List<List<char[]>> chromosomes)
        {
            double[] bestGen = new double[chromosomes.Count];
            List<List<char[]>> chromosomesTemp = chromosomes;
            for (int i = 0; i < chromosomes.Count; i++)
            {
                bestGen[i] = FitnesFunction(chromosomes[i]);

            }
            SortTop(ref bestGen, ref chromosomesTemp);
            chromosomesTemp.RemoveRange(100, chromosomesTemp.Count - 100);

            return chromosomesTemp;
        }
        private double FitnesFunction(List<char[]> chromosome)
        {
            var decryptionOption = SubstitutionCipher(CharContext, chromosome);
            double percent = 0;
            for (int i = 0; i < decryptionOption.Length - 2; i++)
            {
                var threeLetters = decryptionOption.Substring(i, 3);
                if (trigramDictionary.ContainsKey(threeLetters))
                {
                    percent += trigramDictionary[threeLetters];
                }
            }
            var percentSimilarity = percent / (decryptionOption.Length - 2);

            return percentSimilarity;
        }


        private void SortTop(ref double[] bestGen, ref List<List<char[]>> chromosomes)
        {
            bool sorted;
            for (int i = 0; i < bestGen.Length - 1; i++)
            {
                sorted = true;
                for (int j = bestGen.Length - 1; j > i; j--)
                {

                    if (bestGen[j] < bestGen[j - 1])
                    {
                        double temp = bestGen[j - 1];
                        var tempChar = chromosomes[j - 1];
                        bestGen[j - 1] = bestGen[j];
                        chromosomes[j - 1] = chromosomes[j];
                        bestGen[j] = temp;
                        chromosomes[j] = tempChar;
                        sorted = false;
                    }
                }
                if (sorted)
                    break;
            }
            Array.Reverse(bestGen);
            chromosomes.Reverse();
        }

        private List<List<char[]>> GenerateStartPopulation(int numberOfStart)
        {
            var PackChromosomeSet = new List<List<char[]>>();
            Random random = new Random();
            for(int i=0;i<numberOfStart;i++) {
               var  ChromosomeSet = new List<char[]>();
                for (int k = 0; k < KeyCount; k++)
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
                PackChromosomeSet.Add(ChromosomeSet);
            }
            return PackChromosomeSet;
        }
        private List<List<char[]>> GenerateStartPopulation2()
        {
            var PackChromosomeSet = new List<List<char[]>>();
            var ChromosomeSet = new List<char[]>();
            string ab = "TUEIJYSPBCAOHFKNDLMZRGQWXV";
            ChromosomeSet.Add(ab.ToCharArray());
            ab = "YQBJEUNIDPXLWTMRSHOKZCAGVF";
            ChromosomeSet.Add(ab.ToCharArray());
            ab = "QOFPASKZHNLTJUIXCDYRGEWBMV";
            ChromosomeSet.Add(ab.ToCharArray());
            ab = "JLGONRPKCBIHDTUEFSQYXAZVMW";
            ChromosomeSet.Add(ab.ToCharArray());
            PackChromosomeSet.Add(ChromosomeSet);
            return PackChromosomeSet;

        }

            private string SubstitutionCipher(char[] StrangeText, List<char[]> BigChromosome)
        {
            
            var TempDecript = new char[CharContext.Length];
            for (int i = 0; i < StrangeText.Length; i++)
            {
                var letter = StrangeText[i];                
                string chromosomeStr = new string(BigChromosome[i%KeyCount]);
                var indexOfChange = chromosomeStr.IndexOf(letter);
                TempDecript[i] = CryptoLetters[indexOfChange];
            }
            

            return new string(TempDecript);
        }
    }
}
