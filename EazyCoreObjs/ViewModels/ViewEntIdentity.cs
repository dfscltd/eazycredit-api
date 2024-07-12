using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewEntIdentity
    {
        //public long Sequence { get; set; }
        //public string EntityID { get; set; }
        //public string EntityTypeCode { get; set; }
        public string IDTypeCode { get; set; }
        public string IDNo { get; set; }
        public string Issuer { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool PrimaryID { get; set; }
        //public string AddedBy { get; set; }
        public DateTime? IssueDate { get; set; }

    }
}
