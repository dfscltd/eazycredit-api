using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.OtherViewModels
{
    public class ViewNewAccountOpening
    {
        public string CusID { get; set; }
        public string ProdCode { get; set; }
        public string BranchCode { get; set; }
        public string CusName { get; set; }
        public bool Arrears { get; set; }
        public string AccountNo { get; set; }
        public string AgentCode { get; set; } = string.Empty;
    }
}
