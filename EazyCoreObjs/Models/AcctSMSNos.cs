using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("AcctSMSNos", Schema = "dbo")]
    public class AcctSMSNos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequence { get; set; }
        public string AccountNo { get; set; }
        public string SMSNo { get; set; }
        public string EmailNo { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string Name { get; set; }

    }
}
