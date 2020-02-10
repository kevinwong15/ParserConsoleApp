using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ParserConsoleApp
{
    public class TestingExample
    {
        private class Context
        {
            public int Param1 { get; set; }
            public int Param2 { get; set; }
        }

        public void DictionaryApproach(Dictionary<string, object> dict, string formula)
        {
            var expression = new NCalc.Expression(formula);
            expression.Parameters = dict;
            expression.Evaluate();
        }

        public void ReplaceApproachNoLambda(Dictionary<string, object> dict, string formula)
        {
            foreach (var item in dict)
            {
                formula = formula.Replace("[" + item.Key + "]", item.Value.ToString());
            }
            var expression = new NCalc.Expression(formula);
            var output = expression.Evaluate();
        }

        public void ReplaceApproachWithLambda(Dictionary<string, object> dict, string formula)
        {
            foreach(var item in dict)
            {
                formula = formula.Replace("[" + item.Key + "]", item.Value.ToString());
            }
            var expression = new NCalc.Expression(formula);
            var lambda = expression.ToLambda<int>();
            var output = lambda();
        }

        public async Task RunAll()
        {
            var iteration = 1000;
            var unit = 10000;

            var formula = "[Param1] * 7 + [Param2]";
            var dict = new Dictionary<string, object>() { ["Param1"] = 4, ["Param2"] = 9 };

            Stopwatch sw = new Stopwatch();

            sw.Restart();
            for (int k = 0; k < iteration; k++)
            {
                //var output = 4 * 7 + 9;
            }
            sw.Stop();
            Console.WriteLine($"Approach Basic = {sw.ElapsedMilliseconds * unit} ms");

            sw.Restart();
            for (int k = 0; k < iteration; k++)
            {
                var output = Convert.ToInt16(dict["Param1"]) * 7 + Convert.ToInt16(dict["Param2"]);
            }
            sw.Stop();
            Console.WriteLine($"Approach Control = {sw.ElapsedMilliseconds * unit} ms");

            sw.Restart();
            for (int k = 0; k < iteration; k++)
            {
                DictionaryApproach(dict, formula);
            }
            sw.Stop();
            Console.WriteLine($"Dict (No Lamda) = {sw.ElapsedMilliseconds * unit} ms");

            sw.Restart();
            for (int k = 0; k < iteration; k++)
            {
                ReplaceApproachNoLambda(dict, formula);
            }
            sw.Stop();
            Console.WriteLine($"Replace All Digits (No Lamda) = {sw.ElapsedMilliseconds * unit} ms");

            sw.Restart();
            for (int k = 0; k < iteration; k++)
            {
                ReplaceApproachWithLambda(dict, formula);
            }
            sw.Stop();
            Console.WriteLine($"Replace All Digits (With Lamda) = {sw.ElapsedMilliseconds * unit} ms");

            sw.Restart();
            var baseScript = CSharpScript.Create("");

            for (int k = 0; k < iteration; k++)
            {
                object result = await baseScript.ContinueWith("4 * 7 + 9").RunAsync();
            }
            sw.Stop();
            Console.WriteLine($"Rolsyn = {sw.ElapsedMilliseconds * unit} ms");

        }
    }
}