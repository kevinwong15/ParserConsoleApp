using ParserConsoleApp.Enumerations;
using ParserConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserConsoleApp.Algorithms
{
    class TermAlgorithm
    {
        public static Algorithm Create()
        {
            var output = new Algorithm();

            //Load Premium Data
            Dictionary<string, Table> termTables = new Dictionary<string, Table>();

            //Load TermPremiumRate
            var termBaseRate = new Table("TermBaseRate", 4, 5);

            termBaseRate.RowLookup.IndexName = new BenefitInfoType[] { BenefitInfoType.Gender, BenefitInfoType.SmokingStatus };
            termBaseRate.RowLookup.IndexType = new TableIndexType[] { TableIndexType.Exact, TableIndexType.Exact };
            termBaseRate.RowLookup.IndexValue[0] = new object[] { 1, "M", "NS" };
            termBaseRate.RowLookup.IndexValue[1] = (new object[] { 2, "M", "S" });
            termBaseRate.RowLookup.IndexValue[2] = (new object[] { 3, "F", "NS" });
            termBaseRate.RowLookup.IndexValue[3] = (new object[] { 4, "F", "S" });

            termBaseRate.ColumnLookup.IndexName = new BenefitInfoType[] { BenefitInfoType.ANB };
            termBaseRate.ColumnLookup.IndexType = new TableIndexType[] { TableIndexType.Exact };
            termBaseRate.ColumnLookup.IndexValue[0] = new object[] { 1, "35" };
            termBaseRate.ColumnLookup.IndexValue[1] = new object[] { 2, "36" };
            termBaseRate.ColumnLookup.IndexValue[2] = new object[] { 3, "37" };
            termBaseRate.ColumnLookup.IndexValue[3] = new object[] { 4, "38" };
            termBaseRate.ColumnLookup.IndexValue[4] = new object[] { 5, "39" };

            termBaseRate.TableData[0] = new decimal[] { 11, 12, 13, 14, 15 };
            termBaseRate.TableData[1] = new decimal[] { 21, 22, 23, 24, 25 };
            termBaseRate.TableData[2] = new decimal[] { 31, 32, 33, 34, 35 };
            termBaseRate.TableData[3] = new decimal[] { 41, 42, 43, 44, 45 };
            termTables.Add(termBaseRate.Name, termBaseRate);

            //Load LSID Premium Rate
            var termLSIDFactor = new Table("TermLSIDFactor", 4, 1);
            termLSIDFactor.RowLookup.IndexName = new BenefitInfoType[] { BenefitInfoType.SumInsured };
            termLSIDFactor.RowLookup.IndexType = new TableIndexType[] { TableIndexType.Range };
            termLSIDFactor.RowLookup.IndexValue[0] = new object[] { 1, "250000" };
            termLSIDFactor.RowLookup.IndexValue[1] = new object[] { 2, "500000" };
            termLSIDFactor.RowLookup.IndexValue[2] = new object[] { 3, "750000" };
            termLSIDFactor.RowLookup.IndexValue[3] = new object[] { 4, "1000000" };

            termLSIDFactor.TableData[0] = new decimal[] { 1 };
            termLSIDFactor.TableData[1] = new decimal[] { 0.9m };
            termLSIDFactor.TableData[2] = new decimal[] { 0.8m };
            termLSIDFactor.TableData[3] = new decimal[] { 0.7m };
            termTables.Add(termLSIDFactor.Name, termLSIDFactor);

            //Add CalculationSteps
            var calcsteps = new CalculationStep[2][];

            calcsteps[0] = new CalculationStep[] {
                new CalculationStep { CalculationType = CalculationType.Client, Reference = BenefitInfoType.SumInsured.ToString()},
                new CalculationStep { CalculationType = CalculationType.Client, Reference = BenefitInfoType.PerMillUWLoading.ToString()},
                new CalculationStep { CalculationType = CalculationType.Constant, Reference = "Scalar", Scalar = 0.001M}
            };

            calcsteps[1] = new CalculationStep[] {
                new CalculationStep { CalculationType = CalculationType.Client, Reference = BenefitInfoType.SumInsured.ToString()},
                new CalculationStep { CalculationType = CalculationType.Table, Reference = "TermBaseRate"},
                new CalculationStep { CalculationType = CalculationType.Table, Reference = "TermLSIDFactor"},
                new CalculationStep { CalculationType = CalculationType.Constant, Reference = "Scalar", Scalar = 0.001M}
            };

            //Load Data
            output.Tables = termTables;
            output.CalculationSteps = calcsteps;
            
            return output;
        }
    }
}
