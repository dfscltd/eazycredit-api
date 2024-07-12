using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewTransApproval
    {
        public string TransID { get; set; }
        public string ScreenCode { get; set; }
        public string AddedBy { get; set; }
        public string ApprovedBy { get; set; }
        public bool Approved { get; set; }
        public string ApproveRejectionReason { get; set; }
        public string BranchAddedApproved { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIPApproved { get; set; }
        public string TransCode { get; set; }
        public string Reason { get; set; }
        public long Sequence { get; set; } = 0;

    }
}
