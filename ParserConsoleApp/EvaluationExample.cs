using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ParserConsoleApp
{
    public class EvaluationExample
    {
        public int ExpressiveParserMethod(string inputString)
        {
            var expression = new Expressive.Expression(inputString);
            var dict = new Dictionary<string, object>
            {
                ["variable1"] = 2,
                ["variable2"] = 3,
            };
            var result = expression.Evaluate(dict);
            return Convert.ToInt16(result);
        }

        public int NCalcMethod(string inputString)
        {
            var e = new NCalc.Expression(inputString);
            e.Parameters["variable1"] = 2;
            e.Parameters["variable2"] = 3;
            var result2 = e.Evaluate();            
            return Convert.ToInt16(result2);
        }

        public void Run()
        {
            var iteration = 10000;
            Stopwatch sw = new Stopwatch();
            
            //Control
            var dict = new Dictionary<string, int>() { ["variable1"] = 2, ["variable2"] = 3 };
            var inputString = "7 * ([variable1] + 5 * [variable2])";
            sw.Restart();

            for (var k = 0; k < iteration; k++)
            {
                var output = 7 * (dict["variable1"] + 5 * dict["variable2"]);
            };
            sw.Stop();
            Console.WriteLine($"Control took {sw.ElapsedMilliseconds} ms");

            //Loop through Methods
            Dictionary<string, Func<string, int>> functions = new Dictionary<string, Func<string, int>>();
            functions["ExpressiveParserMethod"] = this.ExpressiveParserMethod;
            functions["NCalcMethod"] = this.NCalcMethod;
            foreach (var item in functions)
            {
                sw.Restart();
                for (var m = 0; m < iteration; m++)
                {
                    var output = item.Value(inputString);
                    //Console.WriteLine(output);
                }
                sw.Stop();
                Console.WriteLine($"{item.Key} took {sw.ElapsedMilliseconds} ms");
            }
        }

    }
}