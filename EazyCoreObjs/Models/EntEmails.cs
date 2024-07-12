using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("EntEmails", Schema = "dbo")]
    public class EntEmails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequence { get; set; }
        public string EntityID { get; set; }
        public string EntityTypeCode { get; set; }
        public string EmailAddr { get; set; }
        public bool PrimaryAddr { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string Remarks { get; set; }
    }
}
