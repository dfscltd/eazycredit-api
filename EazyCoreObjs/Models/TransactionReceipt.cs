using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("TransactionReceipt", Schema = "dbo")]
    public class TransactionReceipt
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SeqNo { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string RefAccountNumber { get; set; }
        public string RefAccountName { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionCurrency { get; set; }
        public string TransactionNarration { get; set; }
        public string TransactionReference { get; set; }
        public string TransIndicator { get; set; }
        public string BeneficiaryBank { get; set; }
        public DateTime TransactionDate { get; set; }
    }

}
