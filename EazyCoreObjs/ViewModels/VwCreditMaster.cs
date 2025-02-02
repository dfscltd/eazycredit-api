﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwCreditMaster
    {
        public string CreditID { get; set; }
        public string Description { get; set; }
        public string CreditType { get; set; }
        public string CreditTypeDesc { get; set; }
        public string OperativeAccountNo { get; set; }
        public string OperativeAccountNoDesc { get; set; }
        public string OperativeAccountProdName { get; set; }
        public bool Overdraft { get; set; }
        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public string ProdCatCode { get; set; }
        public string AccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string CusID { get; set; }
        public string DisbursementAccountNo { get; set; }
        public string DisbursementAccountNoDesc { get; set; }
        public decimal AmountGranted { get; set; }
        public decimal EquityContribution { get; set; }
        public DateTime DateApproved { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string TenorType { get; set; }
        public short Tenor { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Rate { get; set; }
        public decimal ArrearsRate { get; set; }
        public short BaseYear { get; set; }
        public decimal Outstanding { get; set; }
        public bool AutoRepayArrears { get; set; }
        public DateTime FirstDisbursementDate { get; set; }
        public decimal FirstDisbursementAmount { get; set; }
        public string LastDisbursementDate { get; set; }
        public string RepaymentType { get; set; }
        public string RepaymentTypeDesc { get; set; }
        public string FixedRepaymentAmount { get; set; }
        public DateTime? FixedRepaymentStartDate { get; set; }
        public string FixedRepaymentFrequency { get; set; }
        public DateTime? FixedRepaymentNextDate { get; set; }
        public DateTime? FixedRepaymentLastDate { get; set; }
        public string FixedRepaymentArrearsTreatment { get; set; }
        public string FixedRepaymentArrearsTreatmentDesc { get; set; }
        public string ConsentStatus { get; set; }
        public string ConsentStatusDesc { get; set; }
        public string PurposeType { get; set; }
        public string PurposeTypeDesc { get; set; }
        public string OwnershipID { get; set; }
        public string OwnershipIDDesc { get; set; }
        public string SecurityCoverageCode { get; set; }
        public string SecurityCoverageCodeDesc { get; set; }
        public string GuaranteeCoverageCode { get; set; }
        public string GuaranteeCoverageCodeDesc { get; set; }
        public string LegalActionCode { get; set; }
        public string LegalActionCodeDesc { get; set; }
        public string CreditCardNo { get; set; }
        public string OnLendingStatus { get; set; }
        public string OnLendingStatusDesc { get; set; }
        public string OnLendingCreditID { get; set; }
        public string Memo { get; set; }
        public DateTime? MaxValueDate { get; set; }
        public DateTime? LoanMaxValueDate { get; set; }
        public bool Active { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public string AddedBy { get; set; }
        public string BranchAdded { get; set; }
        public string BranchName { get; set; }
        public string AddedApprovedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastApprovedBy { get; set; }
        public string BranchAddedApproved { get; set; }
        public string BranchAddedApprovedName { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateAddedApproved { get; set; }
        public DateTime PostingDateLastModified { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime? DateLastApproved { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public TimeSpan? TimeLastApproved { get; set; }
        public string CurrCode { get; set; }
        public string ArrearsAccountNo { get; set; }
        public string BatchNo { get; set; }
        public string BatchDesc { get; set; }
        public decimal UnpaidPrincipal { get; set; }
        public decimal UnpaidInterest { get; set; }
        public bool ObserveLimit { get; set; }
        public string CreditClassCode { get; set; }
        public string CreditClassDesc { get; set; }
        public bool CapitalizeInterest { get; set; }
        public decimal LoanAvailableBal { get; set; }
        public decimal EffectiveRate { get; set; }
        public decimal CarryingAmount { get; set; }
        public bool UpfrontInterest { get; set; }
        public string SegmentID { get; set; }
        public string SegmentName { get; set; }
        public int DaysPastDue { get; set; }
        public decimal DisbursedAmount { get; set; }
        public string TotalScheduledAmount { get; set; }
        public int Installments { get; set; }
        public decimal AmountPaidToDate { get; set; }
        public decimal OverdueEMI { get; set; }
        public decimal ChargesDueToDate { get; set; }
        public decimal TotalOverdue { get; set; }
        public decimal AccruedIntBilled { get; set; }
        public decimal AccruedIntNotBilled { get; set; }
        public decimal NetReceivable { get; set; }
        public decimal PayOffBalance { get; set; }
        public decimal LoanDueMonth { get; set; }
        public decimal LoanRecoveredMonth { get; set; }
        public decimal LoanNotRecoveredMonth { get; set; }
        public decimal LoanRecoveredCumm { get; set; }
        public DateTime? NextDueDate { get; set; }
        public DateTime? LastRepaymentDate { get; set; }
        public string FundSourceCode { get; set; }
        public string FundSourceDesc { get; set; }
        public decimal LastRepaymentAmount { get; set; }
    }
}
