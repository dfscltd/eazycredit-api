using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewRetailFundsTrfLocalSave
    {
        public string TransID { get; set; }
        public string TransCode { get; set; }
        public DateTime ValueDate { get; set; }
        public string AccountNoDr { get; set; }
        public string NarrativeDr { get; set; }
        public string TelephoneDr { get; set; }
        public string AccountNoCr { get; set; }
        public string NarrativeCr { get; set; }
        public string TelephoneCr { get; set; }
        public decimal Amount { get; set; }
        public string CurrCode { get; set; }
        public decimal ExchRate { get; set; }
        public string DocumentNo { get; set; }
        public string BranchAdded { get; set; }
        public string AddedBy { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }
        public int InstrumentNo { get; set; }
        public string AllocRuleCodeDr { get; set; } = "999";
        public string AllocRuleCodeCr { get; set; } = "999";
        public bool InterbankTransfer { get; set; } = false;
        public string MISCodeDr { get; set; } = "ZZZ";
        public string MISCodeCr { get; set; } = "ZZZ";
        public string RefAccountNo { get; set; } = "";
    }
}
