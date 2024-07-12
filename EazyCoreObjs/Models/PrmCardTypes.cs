using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("PrmCardTypes", Schema = "dbo")]
    public class PrmCardTypes
    {
        public string CardType { get; set; }
        public string CardDesc { get; set; }
        public int DigitsNum { get; set; }
        public string ChargeBaseCode { get; set; }
        public decimal CostAmount { get; set; }
        public string AssetAccountNo { get; set; }
        public string ExpenseAccountNo { get; set; }
        public bool HeadOfficeStock { get; set; }
    }
}
