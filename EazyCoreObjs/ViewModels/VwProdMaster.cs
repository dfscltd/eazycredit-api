using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwProdMaster
    {
        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public string ProdCatCode { get; set; }
        public string ProdCatCodeDesc { get; set; }
        public string CurrCode { get; set; }
        public string DebitIntBaseCode { get; set; }
        public string DebitIntBaseCodeDesc { get; set; }
        public string DebitIntAppFreq { get; set; }
        public byte DebitIntAppMonth { get; set; }
        public byte DebitIntAppDay { get; set; }
        public string DebitIntCalcBalType { get; set; }
        public string DebitIntAppMethod { get; set; }
        public string CreditIntBaseCode { get; set; }
        public string CreditIntBaseCodeDesc { get; set; }
        public string CreditIntAppFreq { get; set; }
        public byte CreditIntAppMonth { get; set; }
        public byte CreditIntAppDay { get; set; }
        public string CreditIntCalcBalType { get; set; }
        public string CreditIntAppMethod { get; set; }
        public string COTChargeBaseCode { get; set; }
        public string COTChargeBaseDesc { get; set; }
        public string COTChargeAppFreq { get; set; }
        public byte COTChargeAppMonth { get; set; }
        public byte COTChargeAppDay { get; set; }
        public DateTime LastDebitIntAppDate { get; set; }
        public DateTime LastCreditIntAppDate { get; set; }
        public DateTime LastCOTAppDate { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string BankName { get; set; }
        public string CreditClassSchemeCode { get; set; }
        public string CreditClassSchemeDesc { get; set; }
        public string ArrearsProdCode { get; set; }
        public string ArrearsProdName { get; set; }
        public string SegmentID { get; set; }
        public string SegmentName { get; set; }
    }
}
