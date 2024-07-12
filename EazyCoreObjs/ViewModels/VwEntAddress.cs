using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwEntAddress
    {
        public long Sequence { get; set; }
        public string EntityID { get; set; }
        public string EntityTypeCode { get; set; }
        public string EntityTypeCodeDesc { get; set; }
        public string AddrTypeCode { get; set; }
        public string AddrTypeCodeDesc { get; set; }
        public string Street { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string LocalGovtCode { get; set; }
        public string LocalGovtName { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public bool Dispatch { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
    }
}
