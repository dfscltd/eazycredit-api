using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class CreditTran
    {
        public string TransId { get; set; }
        public DateTime PostingDate { get; set; }
        public string TransCode { get; set; }
        public DateTime ValueDate { get; set; }
        public string AccountNo { get; set; }
        public string ContraAccountNo { get; set; }
        public string MainNarrative { get; set; }
        public string ContraNarrative { get; set; }
        public decimal Amount { get; set; }
        public string CurrCode { get; set; }
        public decimal ExchRate { get; set; }
        public bool ServiceIntFirst { get; set; }
        public string DocumentNo { get; set; }
        public bool? EditMode { get; set; }
        public string TransStatus { get; set; }
        public bool Reversed { get; set; }
        public string BranchAdded { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIpadded { get; set; }
        public string BranchApproved { get; set; }
        public DateTime? PostingDateApproved { get; set; }
        public string AddedApprovedBy { get; set; }
        public DateTime? DateAddedApproved { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIpapproved { get; set; }
        public bool Cancellation { get; set; }

    }
}
