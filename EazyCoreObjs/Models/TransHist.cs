using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("TransHist", Schema = "dbo")]
    public class TransHist
    {
        public long SeqNo { get; set; }
        public string AccountNo { get; set; }
        public string TransCode { get; set; }
        public string UserNarrative { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string ContraAccountNo { get; set; }
        public string RefAccountNo { get; set; }
        public string OtherRefNo { get; set; }
        public string TransID { get; set; }
        public decimal DebitBaseCurr { get; set; }
        public decimal CreditBaseCurr { get; set; }
        public decimal DebitAcctCurr { get; set; }
        public decimal CreditAcctCurr { get; set; }
        public string AcctCurrCode { get; set; }
        public decimal AcctExchRate { get; set; }
        public decimal BalAcctCurr { get; set; }
        public string TransDesc { get; set; }
        public string CurrCode { get; set; }
        public string CurrDesc { get; set; }
        public decimal ExchRate { get; set; }
        public string ModuleCode { get; set; }
        public string ScreenCode { get; set; }
        public string BranchAdded { get; set; }
        public string BranchName { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public DateTime GLPostingDateAdded { get; set; }
        public DateTime ValueDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }
        public string BranchAddedApproved { get; set; }
        public DateTime PostingDateApproved { get; set; }
        public string AddedApprovedBy { get; set; }
        public DateTime DateAddedApproved { get; set; }
        public TimeSpan TimeAddedApproved { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIPApproved { get; set; }
        public bool InternetPub { get; set; }
        public bool Suspicious { get; set; }
        public string SuspiciousReason { get; set; }
        public decimal COTRate { get; set; }
        public bool COTApplied { get; set; }
        public string AllocRuleCode { get; set; }
        public decimal BalBaseCurr { get; set; }
        public string MISCode { get; set; }
        public string MISName { get; set; }
        public string BudgetTransLineCode { get; set; }
    }
}
