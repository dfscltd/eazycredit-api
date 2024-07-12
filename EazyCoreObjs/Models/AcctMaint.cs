using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("AcctMaint", Schema = "dbo")]
    public class AcctMaint
    {

        public string TransID { get; set; }
        public string MaintType { get; set; }
        public DateTime PostingDate { get; set; }
        public string Narrative { get; set; }
        public bool EditMode { get; set; }
        public string TransStatus { get; set; }
        public string BranchAdded { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }
        public string BranchApproved { get; set; }
        public DateTime? PostingDateApproved { get; set; }
        public string AddedApprovedBy { get; set; }
        public DateTime? DateAddedApproved { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIPApproved { get; set; }
    }
}
