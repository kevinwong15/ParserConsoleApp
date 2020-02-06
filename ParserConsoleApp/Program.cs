using System;

namespace ParserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program started..");

            var example = new EvaluationExample();
            example.Run();

            Console.WriteLine("Program finished..");
            Console.ReadLine();
        }
    }
}