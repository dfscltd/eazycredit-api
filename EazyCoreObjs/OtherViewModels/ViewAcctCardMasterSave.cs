﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.OtherViewModels
{
    public class ViewAcctCardMasterSave
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
    }
}
