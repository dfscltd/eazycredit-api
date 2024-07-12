using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("CreditSchedulesLinesMaint", Schema = "dbo")]
    public class CreditSchedulesLinesMaint
    {
        public long Sequence { get; set; }
        public string TransID { get; set; }
        public string CreditID { get; set; }
        public DateTime ValueDate { get; set; }
        public string LineType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ChargeBaseCode { get; set; }

    }
}
