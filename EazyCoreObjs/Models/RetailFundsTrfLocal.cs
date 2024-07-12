using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.Models
{
    public class RetailFundsTrfLocal
    {
        public string TransID { get; set; }
        public DateTime PostingDate { get; set; }
        public string TransCode { get; set; }
        public DateTime ValueDate { get; set; }
        public string AccountNoDr { get; set; }
        public string NarrativeDr { get; set; }
        public string TelephoneDr { get; set; }
        public string AccountNoCr { get; set; }
        public string NarrativeCr { get; set; }
        public string TelephoneCr { get; set; }
        public decimal Amount { get; set; }
        public string CurrCode { get; set; }
        public decimal ExchRate { get; set; }
        public string DocumentNo { get; set; }
        public bool EditMode { get; set; }
        public string TransStatus { get; set; }
        public bool Reversed { get; set; }
        public string BranchAdded { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }
        public string BranchApproved { get; set; }
        public DateTime? PostingDateApproved { get; set; }
        public string AddedApprovedBy { get; set; }
        public DateTime? DateAddedApproved { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIPApproved { get; set; }
        public int InstrumentNo { get; set; }
        public string AllocRuleCodeDr { get; set; }
        public string AllocRuleCodeCr { get; set; }
        public bool InterbankTransfer { get; set; }
        public string SenderAccountNo { get; set; }
        public string SenderAccountName { get; set; }
        public string SenderBank { get; set; }
        public string BeneficiaryAccountNo { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryBank { get; set; }
        public string MISCodeDr { get; set; }
        public string MISCodeCr { get; set; }
        public string RefAccountNo { get; set; }
    }
}
