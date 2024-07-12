using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("PrmRulesNumbers", Schema = "dbo")]
    public class PrmRulesNumber
    {
        public short Parameter { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string ModuleCode { get; set; }
    }
}
