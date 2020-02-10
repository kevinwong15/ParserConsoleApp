using ParserConsoleApp.Enumerations;
using ParserConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ParserConsoleApp.Engines
{
    public class CalculationEngine
    {
        private double VectorAddition(double[] array)
        {
            int vectorSize = Vector<double>.Count;
            var accVector = Vector<double>.Zero;
            int i;

            for (i = 0; i <= array.Length - vectorSize; i += vectorSize)
            {
                var v = new Vector<double>(array, i);
                accVector = Vector.Add(accVector, v);
            }

            var result = Vector.Dot<double>(accVector, Vector<double>.One);
            for (; i < array.Length; i++)
            {
                result += array[i];
            }
            return result;
        }

        private double VectorMultiplication(double[] array)
        {
            int vectorSize = Vector<double>.Count;
            var accVector = Vector<double>.Zero;
            int i;

            for (i = 0; i <= array.Length - vectorSize; i += vectorSize)
            {
                var v = new Vector<double>(array, i);
                accVector = Vector.Multiply(accVector, v);
            }

            var tempArray = new double[Vector<double>.Count];
            accVector.CopyTo(tempArray);
            var result = tempArray.Aggregate(1d, (p, d) => p * d);

            for (; i < array.Length; i++)
            {
                result *= array[i];
            }
            return result;
        }

        public static decimal GetPremium(Dictionary<BenefitInfoType, object> benefitInfo, Algorithm algorithm)
        {
            var layerCount = Convert.ToInt16(benefitInfo[BenefitInfoType.LayerCount]);
            var blockCount = algorithm.CalculationSteps.Length;

            var debugStepOutputs = new string[layerCount][][];
            var debugBlockOutputs = new string[layerCount][];
            //var debugLayersOutputs = new string[layerCount];

            decimal totalPremium = 0;
            for (int i = 0; i < layerCount; i++)
            {
                decimal layerPremium = 0;

                debugStepOutputs[i] = new string[blockCount][];
                //debugBlockOutputs[i] = new string[blockCount];

                for (int j = 0; j < blockCount; j++)
                {
                    var block = algorithm.CalculationSteps[j];
                    decimal blockpremium = 1;
                    var stepCount = block.Length;

                    debugStepOutputs[i][j] = new string[stepCount];

                    for (int k = 0; k < stepCount; k++)
                    {
                        var step = block[k];
                        var cellValue = step.GetCellValue(algorithm.Tables, benefitInfo);
                        debugStepOutputs[i][j][k] = cellValue.ToString();
                        blockpremium *= cellValue;
                    }

                    debugBlockOutputs[i][j] = blockpremium.ToString();
                    layerPremium += blockpremium;
                }

                //debugLayersOutputs[i] = layerPremium.ToString();
                totalPremium += layerPremium;
            }

            //Display Debug
            for (var i = 0; i < layerCount; i++)
            {
                for (var j = 0; j < blockCount; j++)
                {
                    Console.WriteLine($"Layer {i}, Block {j} Step Values = {string.Join("|", debugStepOutputs[i][j])} = {debugBlockOutputs[i][j]}");
                }
            }

            return totalPremium;
        }
    }
}
