using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class CreditCharge: CommonFields
    {
        public long Sequence { get; set; }
        public string TransID { get; set; }
        public string CreditId { get; set; }
        public string ChargeBaseCode { get; set; }
        public decimal Rate { get; set; }
        public string RateType { get; set; }
        public string FreqCode { get; set; }
        public DateTime NextExecDate { get; set; }
        public DateTime? LastExecDate { get; set; }
        public bool? Upfront { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public CreditMaintHist CreditMaintHist { get; set; }

    }
}
