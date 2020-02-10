using Newtonsoft.Json;
using ParserConsoleApp.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserConsoleApp.Models
{
    public class Table
    {
        public string Name { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public Lookup RowLookup { get; set; }
        public Lookup ColumnLookup { get; set; }
        public decimal[][] TableData { get; set; }

        public Table(string name, int rowCount, int columnCount)
        {
            Name = name;
            RowCount = rowCount;
            ColumnCount = columnCount;
            RowLookup = new Lookup(name, rowCount);
            ColumnLookup = new Lookup(name, columnCount);
            TableData = new decimal[rowCount][];
        }
    }

    public class Lookup
    {
        public string Name { get; set; }
        public LookupType LookupType { get; set; }
        public BenefitInfoType[] IndexName { get; set; }
        public TableIndexType[] IndexType { get; set; }
        public object[][] IndexValue { get; set; }
        
        public Lookup(string name, int comboCount)
        {
            Name = name;
            IndexValue = new object[comboCount][];
        }
    }
}