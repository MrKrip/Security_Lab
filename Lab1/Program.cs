using System;
using System.Collections.Generic;
using System.IO;
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
            string path = @"D:\Сесурити\";
            string Context = File.ReadAllText(path+ "Lab1_2.txt");
            Lab1 lab1 = new Lab1();
            lab1.Lab1_2(Context);
        }
    }
}
