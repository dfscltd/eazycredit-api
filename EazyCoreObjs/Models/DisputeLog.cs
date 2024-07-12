using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("DisputeLog", Schema = "dbo")]
    public class DisputeLog
    {
        public long SeqNo { get; set; }
        public string AccountNo { get; set; }
        public string DisputeCode { get; set; } 
        public string Comment { get; set; } 
        public DateTime LogDate { get; set; } 
        public DateTime LogDateTime { get; set; }
        public DateTime PostingDate { get; set; }
        public string ReferenceID { get; set; }
        public bool Resolved { get; set; }
        public DateTime? DateResolved { get; set; }

        public string ResolvedBy { get; set; }
    }
}
