using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwMessagesLogSave
    {
        public long Sequence { get; set; }
        public string MessageText { get; set; }
        public string AccountNo { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddr { get; set; }
        public string MessageType { get; set; }
        public string TransID { get; set; }
        public long TransLineNo { get; set; }
        public decimal TransAmount { get; set; }
        public decimal AvailBalance { get; set; }

    }
}
