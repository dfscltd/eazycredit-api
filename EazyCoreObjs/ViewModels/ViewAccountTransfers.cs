using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewAccountTransfers
    { 
        public string FromCusID { get; set; }  
        public string FromAccountNo { get; set; } 
        public string FromAccountName { get; set; }
        public string DestinationAccountNo { get; set; }
        public string DestinationAccountName { get; set; }
        public string DestinationBankCode { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal TransferAmount { get; set; }
        public string CurrencyCode { get; set; }
        public string Remarks { get; set; }
        public string TransRef { get; set; }
        public string WorkstationAdded { get; set; }
    }
}
