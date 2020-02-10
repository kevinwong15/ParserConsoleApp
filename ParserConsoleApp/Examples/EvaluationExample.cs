using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ParserConsoleApp
{
    public class EvaluationExample
    {
        public int ExpressiveParserMethod(Dictionary<string, object> input, string inputString)
        {
            var expression = new Expressive.Expression(inputString);
            var result = expression.Evaluate(input);
            return Convert.ToInt16(result);
        }

        public int NCalcMethod(Dictionary<string, object> input, string inputString)
        {
            var e = new NCalc.Expression(inputString);
            e.Parameters = input;
            var result2 = e.Evaluate();
            return Convert.ToInt16(result2);
        }

        public class Context
        {
            public int Variable1 { get; set; }
            public int Variable2 { get; set; }

        }

        public int NCalcLamdaMethod(string inputString)
        {
            var e = new NCalc.Expression(inputString);
            var context = new Context { Variable1 = 2, Variable2 = 3 };
            var lambda = e.ToLambda<Context, int>();
            var output = lambda(context);
            return output;
        }

        public int ParameterAccess1(string formula)
        {
            var expression = new NCalc.Expression(formula);
            expression.Parameters["Variable1"] = 4;
            expression.Parameters["Variable2"] = 9;
            var output = expression.Evaluate();
            return Convert.ToInt16(output);
        }

        public int ParameterAccess2(string formula)
        {
            var expression = new NCalc.Expression(formula);
            var lambda = expression.ToLambda<Context, int>();
            var context = new Context { Variable1 = 4, Variable2 = 9 };
            var output = lambda(context);
            return output;
        }

        public void Run()
        {
            Stopwatch sw = new Stopwatch();

            var iteration = 10000;
            //var inputString = "7 * ([Variable1] + 5 * [Variable2])";
            var inputString = "[Variable1] * 7 + [Variable2]";

            //Control
            var dict = new Dictionary<string, object>() { ["Variable1"] = 2, ["Variable2"] = 3 };
            sw.Restart();

            for (var k = 0; k < iteration; k++)
            {
                var output = 7 * (Convert.ToInt16(dict["Variable1"]) + 5 * Convert.ToInt16(dict["Variable2"]));
            };
            sw.Stop();
            Console.WriteLine($"Control took {sw.ElapsedMilliseconds} ms");

            //Loop through Methods
            Dictionary<string, Func<Dictionary<string, object>, string, int>> functions = new Dictionary<string, Func<Dictionary<string, object>, string, int>>
            {
                ["ExpressiveParserMethod"] = this.ExpressiveParserMethod,
                ["NCalcMethod"] = this.NCalcMethod,
                //["NCalcLamdaMethod"] = this.NCalcLamdaMethod,
                //["ParameterAccess1"] = this.ParameterAccess1,
                //["ParameterAccess2"] = this.ParameterAccess2,
            };

            foreach (var item in functions)
            {
                sw.Restart();
                for (var m = 0; m < iteration; m++)
                {
                    var output = item.Value(dict, inputString);
                    //Console.WriteLine(output);
                }
                sw.Stop();
                Console.WriteLine($"{item.Key} took {sw.ElapsedMilliseconds} ms");
            }
        }
    }
}