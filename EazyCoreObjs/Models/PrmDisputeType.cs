using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("PrmDisputeType", Schema = "dbo")]
    public class PrmDisputeType
    {
        public string DisputeCode { get; set; }
        public string DisputeDesc { get; set; }
    }
}
