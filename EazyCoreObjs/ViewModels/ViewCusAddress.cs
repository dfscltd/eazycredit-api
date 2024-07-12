using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewCusAddress
    {
        //public long Sequence { get; set; }
        //public string CusID { get; set; }
        public string AddrTypeCode { get; set; }
        public string Street { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string LocalGovtCode { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool Dispatch { get; set; }
        //public string AddedBy { get; set; }

    }
}
