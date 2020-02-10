using System;
using System.Collections.Generic;
using System.Text;

namespace ParserConsoleApp.Models
{
    public class Algorithm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public CalculationStep[][] CalculationSteps { get; set; }
        public Dictionary<string,Table> Tables { get; set; }
    }
}
