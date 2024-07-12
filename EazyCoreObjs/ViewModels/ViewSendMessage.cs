using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewSendMessage
    {
        public string CustNo { get; set; }
        public string AccountNumber { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string TransID { get; set; }
        public decimal TransAmount { get; set; }
        public decimal AvailBalance { get; set; }
    }
}
