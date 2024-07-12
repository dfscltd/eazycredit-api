using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreditScheduleSummaryDto
    {
        public string AccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string CreditID { get; set; }
        public DateTime ValueDate { get; set; }
        public decimal TotalPrincipal { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal PrinPlusInt { get; set; }
        public decimal Charges { get; set; }
        public decimal Total { get; set; }
        public decimal CummRepayment { get; set; }
        public decimal CummPrincipal { get; set; }
        public decimal OutstandPrincipal { get; set; }

    }
}
