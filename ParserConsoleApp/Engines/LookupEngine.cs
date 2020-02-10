using ParserConsoleApp.Enumerations;
using ParserConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserConsoleApp.Engines
{
    public class LookupEngine
    {
        private static int GetIndex(Dictionary<BenefitInfoType, object> benefitInfo, Lookup lookup)
        {
            IEnumerable<object[]> workingData = lookup.IndexValue;

            //Deal with exact index type
            //note this is ineffective, have a Array listing the position for Exact and loop through that instead
            for (int i = 0; i < lookup.IndexName.Length; i++)
            {
                if (lookup.IndexType[i] == TableIndexType.Exact)
                {
                    var index = i;
                    var lookupValue = benefitInfo[lookup.IndexName[index]].ToString();
                    workingData = workingData.Where(x => x[index + 1].ToString() == lookupValue);
                }
            }

            //Deal with Range index type
            for (int i = 0; i < lookup.IndexName.Length; i++)
            {
                if (lookup.IndexType[i] == TableIndexType.Range)
                {
                    var index = i;
                    var lookupValue = Convert.ToDouble(benefitInfo[lookup.IndexName[index]]);
                    workingData = workingData.OrderByDescending(x => x[0]);
                    var maxCount = workingData.ToList().Count();
                    var k = 0;
                    while (Convert.ToDouble(workingData.ElementAt(k)[index + 1]) > lookupValue)
                    {
                        k++;
                        if (k >= maxCount)
                        {
                            throw new ArgumentException($"Cannot range match {lookupValue} in {lookup.IndexName[index].ToString()} of {lookup.Name}'s {lookup.LookupType}");
                        }
                    }

                    var ouputValue = workingData.ElementAt(k)[index + 1];
                    workingData = workingData.Where(x => x[index + 1].ToString() == ouputValue.ToString());
                }
            }

            //Get final listing
            var variableCount = workingData.ToList().Count;
            if (variableCount == 1)
            {
                var output = Convert.ToInt16(workingData.First()[0]) - 1;
                return output;
            }
            else
            {
                var lookupKeys = lookup.IndexName.Select(x => x.ToString());

                if (variableCount == 0)
                {
                    throw new ArgumentException($"Cannot find lookup in {lookup.Name}'s {lookup.LookupType}");
                }
                else
                {
                    throw new ArgumentException($"Find multiple lookup in {lookup.Name}'s {lookup.LookupType}");
                }
            }
        }

        public static decimal GetValue(Dictionary<BenefitInfoType, object> benefitInfo, Table table)
        {
            var rowIndex = 0;
            var columnIndex = 0;

            if (table.RowCount != 1)
                rowIndex = GetIndex(benefitInfo, table.RowLookup);

            if (table.ColumnCount != 1)
                columnIndex = GetIndex(benefitInfo, table.ColumnLookup);

            var output = table.TableData[rowIndex][columnIndex];
            return output;

        }

    }
}