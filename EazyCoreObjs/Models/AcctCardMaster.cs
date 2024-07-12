using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("AcctCardMaster", Schema = "dbo")]
    public class AcctCardMaster
    {
        public long Sequence { get; set; }
        public string TransID { get; set; }
        public string AccountNo { get; set; }
        public string CardType { get; set; }
        public string CardSubType { get; set; }
        public string CardNo { get; set; }
        public string SecurityNo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BankCode { get; set; }
        public string SerialNo { get; set; }
        public decimal ChargeAmount { get; set; }
        public string CardStatus { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public string AddedBy { get; set; }
        public string AddedApprovedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateAddedApproved { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeAddedApproved { get; set; }
        public TimeSpan TimeLastModified { get; set; }
    }
}
