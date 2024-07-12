﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("AcctMaster", Schema = "dbo")]
    public class AcctMaster
    {
        //[Key]
        public string AccountNo { get; set; }
        public string ParentAccountNo { get; set; }
        public string ParentAccountNoOverdrawn { get; set; }
        public string NetAccountNo { get; set; }
        public string OldAccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string CusId { get; set; }
        public string BranchCode { get; set; }
        public string UnitCode { get; set; }
        public string UnitPosCode { get; set; }
        public string ProdCode { get; set; }
        public string LedgerChartCode { get; set; }
        public string CurrCode { get; set; }
        public int? Sequence { get; set; }
        public int? SequenceSo { get; set; }
        public string AccountStatus { get; set; }
        public string PostStatus { get; set; }
        public string AccountType { get; set; }
        public short? AcctSecurityLevel { get; set; }
        public string PaymentInstruct { get; set; }
        public string PaymentCombin { get; set; }
        public bool? Arrears { get; set; }
        public string MarketedBy { get; set; }
        public DateTime? PostingDateAdded { get; set; }
        public decimal? DebitIntBaseCodeMargin { get; set; }
        public decimal? CreditIntBaseCodeMargin { get; set; }
        public string CotbaseCode { get; set; }
        public DateTime? LastTransDateUser { get; set; }
        public DateTime? LastDebitTransDateUser { get; set; }
        public DateTime? LastCreditTransDateUser { get; set; }
        public DateTime? LastTransDateSystem { get; set; }
        public DateTime? LastDebitTransDateSystem { get; set; }
        public DateTime? LastCreditTransDateSystem { get; set; }
        public decimal? BookBal { get; set; }
        public decimal? ClearBal { get; set; }
        public decimal? CurrBal { get; set; }
        public decimal? BlockedBal { get; set; }
        public decimal? DisbursedAmount { get; set; }
        public decimal? LastDayBookBal { get; set; }
        public decimal? LastDayClearBal { get; set; }
        public decimal? LastDayCurrBal { get; set; }
        public decimal? LastDayMonthDebits { get; set; }
        public decimal? LastDayMonthCredits { get; set; }
        public decimal? LastDayYearDebits { get; set; }
        public decimal? LastDayYearCredits { get; set; }
        public decimal? LastDayDisbursedAmount { get; set; }
        public decimal? LastMonthBookBal { get; set; }
        public decimal? LastMonthClearBal { get; set; }
        public decimal? LastMonthCurrBal { get; set; }
        public decimal? LastYearBookBal { get; set; }
        public decimal? LastYearClearBal { get; set; }
        public decimal? LastYearCurrBal { get; set; }
        public decimal? LastTwoYearsBookBal { get; set; }
        public decimal? LastTwoYearsClearBal { get; set; }
        public decimal? LastTwoYearsCurrBal { get; set; }
        public decimal? MonthDebits { get; set; }
        public decimal? MonthCredits { get; set; }
        public decimal? YearDebits { get; set; }
        public decimal? YearCredits { get; set; }
        public string CreditClassCode { get; set; }
        public bool StopInterestApplication { get; set; }
        public string AddedBy { get; set; }
        public string AddedApprovedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastApprovedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateAddedApproved { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime? DateLastApproved { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TimeSpan? TimeAdded { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TimeSpan TimeLastModified { get; set; }
        public TimeSpan? TimeLastApproved { get; set; }
        public string ClosedBy { get; set; }
        public string ClosedApprovedBy { get; set; }
        public DateTime? DateClosed { get; set; }
        public DateTime? TimeClosed { get; set; }
        public DateTime? PostingDateClosed { get; set; }
        public decimal? CalcField1 { get; set; }
        public decimal? CalcField2 { get; set; }
        public decimal? ArrearsPrincipalToDate { get; set; }
        public decimal? ArrearsInterestToDate { get; set; }
        public decimal? ArrearsSettlementToDate { get; set; }
        public decimal? UnpaidPrincipal { get; set; }
        public decimal? UnpaidInterest { get; set; }
        public decimal? YearOpeningBalance { get; set; }
        public bool? EnableSms { get; set; }
        public bool? EnableEmail { get; set; }
        public bool? EnableInternet { get; set; }
        public bool? InternetPub { get; set; }
        public string PanNo { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public DateTime? CardExipryDate { get; set; }
        public decimal? CardIssueCounter { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public decimal CardseqNo { get; set; }
        public DateTime? CreditExpiryDate { get; set; }
        public int? CreditClassDays { get; set; }
        public decimal? ArrearsPrincipalToday { get; set; }
        public decimal? ArrearsInterestToday { get; set; }
        public decimal? ArrearsSettlementToday { get; set; }
        //public decimal? CardIssueCounterss { get; set; }
        public decimal? CarryingCurrClearBal { get; set; }
        public decimal? Impairment { get; set; }
        public string SegmentId { get; set; }
        public DateTime? ArrearsLastRepaymentDate { get; set; }
        public decimal? ArrearsLastRepaymentAmount { get; set; }
        public decimal? ArrearsNumber { get; set; }
        public decimal? MinBalMargin { get; set; }
        public string MISOfficeCode { get; set; } = "ZZZ";
        public bool Moratorium { get; set; } = false;
        //public string TierCode { get; set; }

    }

}
