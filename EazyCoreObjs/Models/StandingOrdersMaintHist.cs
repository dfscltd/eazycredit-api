using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("StandingOrdersMaintHist", Schema = "dbo")]
    public class StandingOrdersMaintHist
    {
        public string TransID { get; set; }
        public string StandingOrderNo { get; set; }
        public string StandingOrderType { get; set; }
        public string AccountNo { get; set; }
        public string ContraAccountNo { get; set; }
        public string Description { get; set; }
        public string Narrative { get; set; }
        public string FrequencyCode { get; set; }
        public bool ObserveLimit { get; set; }
        public decimal FixedAmount { get; set; }
        public int MaximumNumber { get; set; }
        public decimal MaximumAmount { get; set; }
        public DateTime MaximumDate { get; set; }
        public decimal MaximumThreshhold { get; set; }
        public int ExecutedNumber { get; set; }
        public decimal ExecutedAmount { get; set; }
        public DateTime FirstExecDate { get; set; }
        public DateTime NextExecDate { get; set; }
        public DateTime LastExecDate { get; set; }
        public bool EditMode { get; set; }
        public string TransStatus { get; set; }
        public bool Discontinued { get; set; }
        public string AddedBy { get; set; }
        public string BranchAdded { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateAddedApproved { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeAddedApproved { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIPAdded { get; set; }
        public string BranchAddedApproved { get; set; }
        public DateTime PostingDateApproved { get; set; }
        public string AddedApprovedBy { get; set; }
        public string WorkstationApproved { get; set; }
        public string WorkstationIPApproved { get; set; }
        public string MISCode { get; set; }
    }
}
