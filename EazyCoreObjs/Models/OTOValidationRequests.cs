using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("OTOValidationRequests", Schema = "dbo")]
    public class OTOValidationRequests
    {
        public long Sequence { get; set; }
        public string AccountNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddr { get; set; }
        public string Reason { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DateTimeRequest { get; set; }
    }
}
