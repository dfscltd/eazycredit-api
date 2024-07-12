using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwTransLinesCopy
    {
        public string TransIdFrom { get; set; }
        public string TransIdTo { get; set; }
        public string CopyOption { get; set; }
        public bool ClearBatch { get; set; }
        public decimal Charge { get; set; }
        public string ProdCode { get; set; }
        public string Narration { get; set; }
    }
}
