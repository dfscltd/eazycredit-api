using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwCreditMasterStatement
    {
        public long SeqNo { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public DateTime ValueDate { get; set; }
        public string AccountNo { get; set; }
        public string TransCode { get; set; }
        public string UserNarrative { get; set; }
        public decimal BalBfAcctCurr { get; set; }
        public decimal DebitAcctCurr { get; set; }
        public decimal CreditAcctCurr { get; set; }
        public decimal BalAcctCurr { get; set; }
        public string BankName { get; set; }
        public string AccountBranchName { get; set; }
        public string AccountName { get; set; }
        public string AccountStreet { get; set; }
        public string AccountCity { get; set; }
        public string ProdName { get; set; }
        public string AccountCurrDesc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreditID { get; set; }
        public decimal AmountGranted { get; set; }
        public int Tenor { get; set; }
        public string TenorType { get; set; }
        public DateTime FirstDisbursementDate { get; set; }
        public decimal AmountDisbursed { get; set; }
        public decimal TotalScheduledAmount { get; set; }
        public int Installments { get; set; }
        public decimal AmountPaidToDate { get; set; }
        public decimal PrincipalOutstanding { get; set; }
        public decimal OverdueEMIs { get; set; }
        public decimal ChargesDueToDate { get; set; }
        public decimal TotalOverdue { get; set; }
        public decimal AccruedIntToDate { get; set; }
        public decimal PayOffBalance { get; set; }
        public string TransID { get; set; }
        public string AddedBy { get; set; }
        public string AddedApprovedBy { get; set; }

    }
}
