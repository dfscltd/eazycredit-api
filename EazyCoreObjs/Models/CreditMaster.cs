using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("CreditMaster", Schema = "dbo")]
    public class CreditMaster
    {
        public string CreditId { get; set; }
        public string Description { get; set; }
        public string CreditType { get; set; }
        public string OperativeAccountNo { get; set; }
        public bool Overdraft { get; set; }
        public string ProdCode { get; set; }
        public string AccountNo { get; set; }
        public string DisbursementAccountNo { get; set; }
        public string ArrearsAccountNo { get; set; }
        public decimal? AmountGranted { get; set; }
        public decimal? EquityContribution { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string TenorType { get; set; }
        public short? Tenor { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? Rate { get; set; }
        public decimal? ArrearsRate { get; set; }
        public short BaseYear { get; set; }
        public bool? AutoRepayArrears { get; set; }
        public DateTime? FirstDisbursementDate { get; set; }
        public decimal? FirstDisbursementAmount { get; set; }
        public DateTime? LastDisbursementDate { get; set; }
        public string RepaymentType { get; set; }
        public decimal? FixedRepaymentAmount { get; set; }
        public DateTime? FixedRepaymentStartDate { get; set; }
        public string FixedRepaymentFrequency { get; set; }
        public DateTime? FixedRepaymentNextDate { get; set; }
        public DateTime? FixedRepaymentLastDate { get; set; }
        public string FixedRepaymentArrearsTreatment { get; set; }
        public string ConsentStatus { get; set; }
        public string PurposeType { get; set; }
        public string OwnershipId { get; set; }
        public string SecurityCoverageCode { get; set; }
        public string GuaranteeCoverageCode { get; set; }
        public string LegalActionCode { get; set; }
        public string CreditCardNo { get; set; }
        public string OnLendingStatus { get; set; }
        public string OnLendingCreditId { get; set; }
        public bool? Active { get; set; }
        public string Memo { get; set; }
        public bool? ObserveLimit { get; set; }
        public DateTime? MaxValueDate { get; set; }
        public DateTime? LastIntAppDate { get; set; }
        public DateTime? PostingDateAdded { get; set; }
        public string AddedBy { get; set; }
        public string BranchAdded { get; set; }
        public string AddedApprovedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastApprovedBy { get; set; }
        public string BranchAddedApproved { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateAddedApproved { get; set; }
        public DateTime? PostingDateLastModified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastApproved { get; set; }
        public TimeSpan? TimeAdded { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        public TimeSpan? TimeLastModified { get; set; }
        public TimeSpan? TimeLastApproved { get; set; }
        public bool? Migrated { get; set; }
        public decimal? UnpaidPrincipal { get; set; }
        public decimal? UnpaidInterest { get; set; }
        public bool? CapitalizeInterest { get; set; }
        public decimal? EffectiveRate { get; set; }
        public decimal? CarryingAmount { get; set; }
        public bool? UpfrontInterest { get; set; }
        public string SegmentId { get; set; }
        public int? DaysPastDue { get; set; }
        public decimal? DisbursedAmount { get; set; }
        public decimal? TotalScheduledAmount { get; set; }
        public int? Installments { get; set; }
        public decimal? AmountPaidToDate { get; set; }
        public decimal? OverdueEmi { get; set; }
        public decimal? ChargesDueToDate { get; set; }
        public decimal? TotalOverdue { get; set; }
        public decimal? AccruedIntBilled { get; set; }
        public decimal? AccruedIntNotBilled { get; set; }
        public decimal? NetReceivable { get; set; }
        public decimal? PayOffBalance { get; set; }
        public decimal? LastDayAmountPaidToDate { get; set; }
        public decimal? LastDayAccruedIntBilled { get; set; }
        public decimal? LoanDueMonth { get; set; }
        public decimal? LoanRecoveredMonth { get; set; }
        public decimal? LoanNotRecoveredMonth { get; set; }
        public decimal? LoanRecoveredCumm { get; set; }
        public DateTime? NextDueDate { get; set; }
        public DateTime? LastRepaymentDate { get; set; }
        public string FundSourceCode { get; set; }

    }
}
