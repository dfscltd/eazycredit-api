using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.Models
{
    public class TransApprovalLog
    {
        public long Sequence { get; set; }
        public string TransID { get; set; }
        public string TransCode { get; set; }
        public string Reason { get; set; }
        public string ScreenCode { get; set; }
        public string TransStatus { get; set; }
        public bool Discontinued { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public string BranchAdded { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string BranchApproved { get; set; }
        public DateTime PostingDateApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DateApproved { get; set; }
        public TimeSpan TimeApproved { get; set; }
        public string ApproveRejectReason { get; set; }
    }
}
