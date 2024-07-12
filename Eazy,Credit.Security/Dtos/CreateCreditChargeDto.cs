using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateCreditChargeDto
    {
        public long Sequence { get; set; }
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string ChargeBaseCode { get; set; }
        public decimal Rate { get; set; }
        public string RateType { get; set; }
        public string FreqCode { get; set; }
        public DateTime NextExecDate { get; set; }
        public bool? Upfront { get; set; }
        public string AddedBy { get; set; }
        public DateTime PostingDateAdded { get; set; }
    }
}
