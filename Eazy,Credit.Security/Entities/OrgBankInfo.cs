using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class OrgBankInfo
    {
        public short Record {  get; set; }
        public string BankCode {  get; set; }
        public string BankName {  get; set; }
        public string Street {  get; set; }
        public string City {  get; set; }
        public string Website {  get; set; }
        public string Email {  get; set; }
        public string Telephones { get; set; }
        public string CurrCode { get; set; }
    }
}
