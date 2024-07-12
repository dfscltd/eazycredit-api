using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwAcctMasterBalance
    {
        public string AccountNo { get; set; }
        public string ParentAccountNo { get; set; }
        public string NetAccountNo { get; set; }
        public string OldAccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string CusID { get; set; }
        public string CusName { get; set; }
        public string BvnNo { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string UnitHeadName { get; set; }
        public string UnitPosCode { get; set; }
        public string UnitPosName { get; set; }
        public string UnitPosOccupantName { get; set; }
        public string Introducer { get; set; }
        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public string ProdCatCode { get; set; }
        public string ProdCatDesc { get; set; }
        public string LedgerChartCode { get; set; }
        public string CurrCode { get; set; }
        public string CurrDesc { get; set; }
        public int Sequence { get; set; }
        public string AccountStatus { get; set; }
        public string AccountStatusDesc { get; set; }
        public string PostStatus { get; set; }
        public string PostStatusDesc { get; set; }
        public string AccountType { get; set; }
        public short AcctSecurityLevel { get; set; }
        public string PaymentInstruct { get; set; }
        public string PaymentCombin { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public decimal DebitIntBaseCodeMargin { get; set; }
        public decimal CreditIntBaseCodeMargin { get; set; }
        public decimal DebitIntRate { get; set; }
        public decimal CreditIntRate { get; set; }
        public decimal COTRate { get; set; }
        public decimal EffectiveDebitIntRate { get; set; }
        public decimal EffectiveCreditIntRate { get; set; }
        public string COTBaseCode { get; set; }
        public bool StopInterestApplication { get; set; }
        public string AddedBy { get; set; }
        public string AddedApprovedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastApprovedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateAddedApproved { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime DateLastApproved { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeAddedApproved { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public TimeSpan TimeLastApproved { get; set; }
        public bool EnableSMS { get; set; }
        public bool EnableEmail { get; set; }
        public bool EnableInternet { get; set; }
        public string SMSNo { get; set; }
        public string EmailNo { get; set; }
        public decimal LastDayBookBal { get; set; }
        public decimal LastDayClearBal { get; set; }
        public decimal LastDayCurrBal { get; set; }
        public decimal BookBal { get; set; }
        public decimal ClearBal { get; set; }
        public decimal CurrBal { get; set; }
        public decimal TodayDebits { get; set; }
        public decimal TodayCredits { get; set; }
        public decimal BlockedBal { get; set; }
        public decimal CreditArrearsBlocked { get; set; }
        public decimal CreditArrears { get; set; }
        public decimal ExcessInterest { get; set; }
        public decimal ArrearsUnappliedInt { get; set; }
        public decimal ApprovedCredit { get; set; }
        public decimal MinBal { get; set; }
        public decimal Uncleared { get; set; }
        public decimal UnappliedDebitInt { get; set; }
        public decimal UnappliedCreditInt { get; set; }
        public decimal UnappliedCOT { get; set; }
        public decimal UnappliedVAT { get; set; }
        public decimal UnaapliedCharges { get; set; }
        public decimal PendingDebits { get; set; }
        public decimal AccruedSMSCharge { get; set; }
        public decimal NetEffectiveBal { get; set; }
        public decimal AvailableBal { get; set; }
        public decimal TerminationBal { get; set; }
        public decimal LoanPayOff { get; set; }

    }
}
