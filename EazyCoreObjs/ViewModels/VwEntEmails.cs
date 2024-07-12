using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwEntEmails
    {
        public long Sequence { get; set; }
        public string EntityID { get; set; }
        public string EntityTypeCode { get; set; }
        public string EntityTypeCodeDesc { get; set; }
        public string EmailAddr { get; set; }
        public string PrimaryAddr { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        //public DateTime DateAdded { get; set; }
        //public DateTime DateLastModified { get; set; }
        //public TimeSpan TimeAdded { get; set; }
        //public TimeSpan TimeLastModified { get; set; }
        public string Remarks { get; set; }

    }
}
