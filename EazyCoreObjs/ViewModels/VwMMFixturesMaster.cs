using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwMMFixturesMaster
    {
        public string AccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string CusID { get; set; }
        public string CusName { get; set; }
        public string FundingAccountNo { get; set; }
        public string FundingAccountNoDesc { get; set; }
        public string TerminateAccountNo { get; set; }
        public string TerminateAccountNoDesc { get; set; }
        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public string ProdCatCode { get; set; }
        public decimal InitialPrincipal { get; set; }
        public decimal Outstanding { get; set; }
        public string CurrCode { get; set; }
        public decimal ExchRate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string TenorType { get; set; }
        public Int16 Tenor { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Rate { get; set; }
        public short BaseYear { get; set; }
        public string DealType { get; set; }
        public string DealTypeDesc { get; set; }
        public string MaturityType { get; set; }
        public string MaturityTypeDesc { get; set; }
        public decimal WHTRate { get; set; }
        public string MarketedBy { get; set; }
        public DateTime PostingDate { get; set; }
        public bool Active { get; set; }
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
        public DateTime DateLastApproved { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public TimeSpan TimeLastApproved { get; set; }
        public decimal MaturityInt { get; set; }
        public decimal WHTAmount { get; set; }
        public decimal MaturityNetInt { get; set; }
        public string BranchUnitPosCode { get; set; }
        public decimal EffectiveRate { get; set; }
        public bool UseEffectiveRate { get; set; }
        public string FundSource { get; set; }
        public string FundSourceDesc { get; set; }
        public decimal RatePerMonth { get; set; }
        public decimal RatePerAnnum { get; set; }
    }

}
