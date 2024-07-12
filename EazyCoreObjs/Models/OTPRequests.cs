using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("OTPRequests", Schema = "dbo")]
    public class OTPRequests
    {
        public long Sequence { get; set; }
        public int Token { get; set; }
        public string MessageText { get; set; }
        public string AccountNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddr { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateTimeGenerated { get; set; }
        public DateTime DateTimeExpiry { get; set; }
        public bool Sent { get; set; }
        public string Status { get; set; } //indicate if successfully validated, expired etc

    }
}
