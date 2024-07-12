using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwAcctCardMaster 
    {
        public long Sequence { get; set; }
        public string TransID { get; set; }
        public string AccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public string CardType { get; set; }
        public string CardDesc { get; set; }
        public string CardSubType { get; set; }
        public string CardSubTypeDesc { get; set; }
        public string CardNo { get; set; }
        public string SecurityNo { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string SerialNo { get; set; }
        public decimal ChargeAmount { get; set; }
        public string CardStatus { get; set; }
        public string CardStatusDesc { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public string AddedBy { get; set; }
        public string AddedApprovedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string DateAdded { get; set; }
        public string DateAddedApproved { get; set; }
        public string DateLastModified { get; set; }
        public string TimeAdded { get; set; }
        public string TimeAddedApproved { get; set; }
        public string TimeLastModified { get; set; }

    }
}
