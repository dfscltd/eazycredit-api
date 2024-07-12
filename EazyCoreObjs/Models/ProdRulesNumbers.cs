using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("ProdRulesNumbers", Schema = "dbo")]
    public class ProdRulesNumbers
    {
        public string ProdCode { get; set; }
        public byte ProdNumberRuleNo { get; set; }
        public decimal ProdNumberRuleValue { get; set; }
    }
}
