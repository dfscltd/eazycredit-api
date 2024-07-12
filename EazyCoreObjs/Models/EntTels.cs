using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("EntTels", Schema = "dbo")]
    public class EntTels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequence { get; set; }
        public string EntityID { get; set; }
        public string EntityTypeCode { get; set; }
        public string TelTypeCode { get; set; }
        public string TelNo { get; set; }
        public string Extension { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string Remarks { get; set; }
    }
}
