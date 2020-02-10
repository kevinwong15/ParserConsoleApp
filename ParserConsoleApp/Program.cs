using Microsoft.CodeAnalysis.CSharp.Scripting;
using Newtonsoft.Json;
using ParserConsoleApp.Algorithms;
using ParserConsoleApp.Engines;
using ParserConsoleApp.Enumerations;
using ParserConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program started..");
            Console.WriteLine();

            //Load client
            Dictionary<BenefitInfoType, object> clientInfo = new Dictionary<BenefitInfoType, object>();
            clientInfo[BenefitInfoType.Name] = "Kevin";
            clientInfo[BenefitInfoType.Gender] = "M";
            clientInfo[BenefitInfoType.SmokingStatus] = "NS";
            clientInfo[BenefitInfoType.PerMillUWLoading] = 100;
            clientInfo[BenefitInfoType.ANB] = 35;
            clientInfo[BenefitInfoType.SumInsured] = new double[] { 1000000, 50 };

            //Create algorithm dictionary
            var algorithms = new Dictionary<string, Algorithm>();
            algorithms.Add("Term", TermAlgorithm.Create());
            algorithms.Add("TPD", TPDAlgorithm.Create());

            //Get Premium
            var totalPremium = CalculationEngine.GetPremium(clientInfo, algorithms["Term"]);
            Console.WriteLine($"Premium = {totalPremium}");

            Console.WriteLine();
            Console.WriteLine("Program finished..");
            Console.ReadLine();
        }
    }
}