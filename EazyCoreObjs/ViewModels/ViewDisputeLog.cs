using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewDisputeLog
    {
        public long SeqNo { get; set; }
        public string AccountNo { get; set; }
        public string DisputeCode { get; set; }
        public string Comment { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime LogDateTime { get; set; }
        public DateTime PostingDate { get; set; }
        public string ReferenceID { get; set; }
    }
}
