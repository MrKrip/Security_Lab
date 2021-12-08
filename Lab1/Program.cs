using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\security\Security_Lab_1\Lab1\cipher\";                
          

            string Context = File.ReadAllText(path + "Lab1_3.txt");
            Lab1 lab1 = new Lab1();
           // lab1.Lab1_2(Context);
            //lab1.Lab1_3(Context);
            lab1.lab_4(Context);
        }
    }
}
