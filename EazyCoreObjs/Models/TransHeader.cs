using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("TransHeaders", Schema = "dbo")]
    public class TransHeader
    {
        [Key]
        public string TransID { get; set; }
        public string TransDesc { get; set; }
        public string CurrCode { get; set; }
        public decimal ExchRate { get; set; }
        public string ModuleCode { get; set; }
        public string ScreenCode { get; set; }
        public string BranchAdded { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public DateTime GLPostingDateAdded { get; set; }
        public DateTime ValueDate { get; set; }
        public bool EditMode { get; set; }
        public string TransStatus { get; set; }
        public bool Reversed { get; set; }
        public bool Posted { get; set; }
        public bool PostedGL { get; set; }
        public bool PostedIB { get; set; }
        public bool PostedPosAccts { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }
        public string BranchAddedApproved { get; set; }
        public DateTime PostingDateApproved { get; set; }
        public string AddedApprovedBy { get; set; }
        public DateTime DateAddedApproved { get; set; }
        public TimeSpan TimeAddedApproved { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIPApproved { get; set; }
        public string ReversalReason { get; set; }
    }
}
