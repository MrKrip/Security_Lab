using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadPassStatisitic RPS25 = new ReadPassStatisitic("D:\\security\\Security_Lab_1\\Lab4\\Document\\25passwords.txt");
            ReadPassStatisitic RPSmany = new ReadPassStatisitic("D:\\security\\Security_Lab_1\\Lab4\\Document\\10-million-password-list-top-100000.txt");
            SuperRandomMegaPasswordGeneratorEver SRMPGE = new SuperRandomMegaPasswordGeneratorEver();
            
            HumanLikePassword HLP = new HumanLikePassword();            
            HLP.GeneratePass(20);

        }
    }
}
