using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class CreditSchedulesLinesIntTemp
    {
        public long Sequence { get; set; }

        public string TransID { get; set; }
        public DateTime InterestDate { get; set; }
        public DateTime ValueDate { get; set; }
        public decimal Amount { get; set; }
        public int Days { get; set; }
        public decimal Rate { get; set; }
        public decimal Interest { get; set; }

    }
}
