using ParserConsoleApp.Engines;
using ParserConsoleApp.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserConsoleApp.Models
{
    public class CalculationStep
    {
        public CalculationType CalculationType { get; set; }
        public string Reference { get; set; }
        public decimal Scalar { get; set; } = 1;

        public decimal GetCellValue(Dictionary<string, Table> tableDb, Dictionary<BenefitInfoType, object> benefitInfo)
        {
            decimal output = 0;
            switch (CalculationType)
            {
                case CalculationType.Client:
                    Enum.TryParse(Reference, out BenefitInfoType clientType);
                    output = Convert.ToDecimal(benefitInfo[clientType]);
                    break;
                case CalculationType.Table:
                    var table = tableDb[Reference];
                    output = LookupEngine.GetValue(benefitInfo,table);
                    break;
                case CalculationType.Constant:
                    output = Scalar;
                    break;
                default:
                    break;
            }
            return output;
        }
    }
}