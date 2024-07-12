using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("CreditSchedulesLines", Schema = "dbo")]
    public class CreditSchedulesLines
    {
        public long Sequence { get; set; }
        public string CreditId { get; set; }
        public DateTime? ValueDate { get; set; }
        public string LineType { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public string ChargeBaseCode { get; set; }
        public bool Executed { get; set; }
    }
}
