using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtentionLibrary;


namespace ExtentionMethodProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your sentence:");

            int result = Console.ReadLine().WordCount();// call extention method

            Console.WriteLine("Sentence has {0} words :)", result);
            Console.ReadKey();
        }
    }
}
