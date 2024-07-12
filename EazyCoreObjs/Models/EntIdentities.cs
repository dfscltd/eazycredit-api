using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("EntIdentities", Schema = "dbo")]
    public class EntIdentities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequence { get; set; }
        public string EntityID { get; set; }
        public string EntityTypeCode { get; set; }
        public string IDTypeCode { get; set; }
        public string IDNo { get; set; }
        public string Issuer { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool PrimaryID { get; set; }
        public string AddedBy { get; set; }
        public string LastmodifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastDateModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan LastTimeModified { get; set; }
        public DateTime? IssueDate { get; set; }
    }
}
