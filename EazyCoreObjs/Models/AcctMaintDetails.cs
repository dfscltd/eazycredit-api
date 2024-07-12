using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("AcctMaintDetails", Schema = "dbo")]
    public class AcctMaintDetails
    {
        [Key]
        public long SeqNo { get; set; }
        public string TransID { get; set; }
        public string AccountNo { get; set; }
        public string Reason { get; set; }
        public string OldTextValue { get; set; }
        public string NewTextValue { get; set; }
        public decimal OldNumValue { get; set; }
        public decimal NewNumValue { get; set; }
        public bool OldBoolValue { get; set; }
        public bool NewBoolValue { get; set; }
        public string Remarks { get; set; }
    }
}
