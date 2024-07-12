using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("PrmRulesText", Schema = "dbo")]
    public class PrmRulesText
    {
        public short Parameter { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string ModuleCode { get; set; }
    }
}
