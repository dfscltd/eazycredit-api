using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwAcctMasterStatementMobileApp
    {
        public long SeqNo { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public DateTime ValueDate { get; set; }
        public string AccountNo { get; set; }
        public string OtherRefNo { get; set; }
        public string UserNarrative { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string CurrCode { get; set; }
        public string CurrDesc { get; set; }
        public decimal ExchRate { get; set; }
        public decimal BfwdAcctCurr { get; set; }
        public decimal DebitAcctCurr { get; set; }
        public decimal CreditAcctCurr { get; set; }
        public string AcctCurrCode { get; set; }
        public decimal AcctExchRate { get; set; }
        public decimal BalAcctCurr { get; set; }
        public string ContraAccountNo { get; set; }
        public string TransID { get; set; }
        public string TransDesc { get; set; }
        public string AddedBy { get; set; }
        public string AddedApprovedBy { get; set; }
        public string BranchAdded { get; set; }
        public string BranchName { get; set; }
        public string BankName { get; set; }
        public string AccountBranchName { get; set; }
        public string AccountName { get; set; }
        public string AccountStreet { get; set; }
        public string AccountCity { get; set; }
        public string ProdName { get; set; }
        public string AccountCurrDesc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RefAccountNo { get; set; }
        public string RefAccountName { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }

    }
}
