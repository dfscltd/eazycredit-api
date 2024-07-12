using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("AcctMandates", Schema = "dbo")]
    public class AcctMandates
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequence { get; set; }
        public string AccountNo { get; set; }
        public string FullName { get; set; }
        //[NotMapped]
        public byte[] Signature { get; set; }
        //[NotMapped]
        public byte[] Photo { get; set; }
        //[NotMapped]
        public byte[] Image { get; set; }
        public string Comments { get; set; }
        public string SignClass { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string account_no { get; set; }
        public string AccountNoOld { get; set; }
        public string BvnNo { get; set; }
        public string TransID { get; set; }
        public DateTime? BirthDate { get; set; }

    }
}
