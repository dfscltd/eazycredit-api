using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwTransHeaderSave
    {
        public string TransID { get; set; }
        public string TransDesc { get; set; }
        public string CurrCode { get; set; }
        public decimal ExchRate { get; set; }
        public string ModuleCode { get; set; }
        public string ScreenCode { get; set; }
        public string AddedBy { get; set; }
        public string BranchAdded { get; set; }
        public DateTime ValueDate { get; set; }
        public string TransStatus { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }

    }
}
