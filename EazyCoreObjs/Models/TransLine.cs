using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("TransLines", Schema = "dbo")]
    public class TransLine
    {
        [Key]
        public long SeqNo { get; set; }
        public string AccountNo { get; set; }
        public string TransCode { get; set; }
        public string UserNarrative { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string ContraAccountNo { get; set; }
        public string RefAccountNo { get; set; }
        public string OtherRefNo { get; set; }
        public string TransID { get; set; }
        public decimal DebitBaseCurr { get; set; }
        public decimal CreditBaseCurr { get; set; }
        public decimal DebitAcctCurr { get; set; }
        public decimal CreditAcctCurr { get; set; }
        public string AcctCurrCode { get; set; }
        public decimal AcctExchRate { get; set; }
        public DateTime ValueDate { get; set; }
        public string AllocRuleCode { get; set; }
        public string MISCode { get; set; }
    }
}
