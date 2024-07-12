using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.Models
{
    public class ProdInvestRateGrid
    {
        public long SeqNo { get; set; }
        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public decimal MinInvestAmount { get; set; }
        public decimal MaxInvestAmount { get; set; }
        public int TenorInDays { get; set; }
        public decimal Rate { get; set; }
    }
}
