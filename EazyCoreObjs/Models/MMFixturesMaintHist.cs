using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("MMFixturesMaintHist", Schema = "dbo")]
    public class MMFixturesMaintHist
    {
        public string TransID { get; set; }
        public string AccountNo { get; set; }
        public string CusID { get; set; }
        public string FundingAccountNo { get; set; }
        public string TerminateAccountNo { get; set; }
        public string TransCode { get; set; }
        public string ProdCode { get; set; }
        public string Narrative { get; set; }
        public decimal Principal { get; set; }
        public string CurrCode { get; set; }
        public decimal ExchRate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string TenorType { get; set; }
        public short Tenor { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Rate { get; set; }
        public short BaseYear { get; set; }
        public string DealType { get; set; }
        public string MaturityType { get; set; }
        public decimal WHTRate { get; set; }
        public string DocumentNo { get; set; }
        public string MarketedBy { get; set; }
        public DateTime PostingDate { get; set; }
        public bool EditMode { get; set; }
        public string TransStatus { get; set; }
        public bool Reversed { get; set; }
        //public bool Active { get; set; }
        public string AddedBy { get; set; }
        public string BranchAdded { get; set; }
        public string AddedApprovedBy { get; set; }
        public DateTime PostingDateApproved { get; set; }
        public string BranchAddedApproved { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateAddedApproved { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeAddedApproved { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIPApproved { get; set; }
        public string BranchUnitPosCode { get; set; }
        public decimal EffectiveRate { get; set; }
        public bool UseEffectiveRate { get; set; }
        public string FundSource { get; set; }
        public string MISOfficeCode { get; set; } = "ZZZ";
    }
}
