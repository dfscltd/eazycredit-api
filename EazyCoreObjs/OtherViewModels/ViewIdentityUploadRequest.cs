using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.OtherViewModels
{
    public class ViewIdentityUploadRequest
    {
        public string AccountNo { get; set; }
        public string IDTypeCode { get; set; }
        public string Issuer { get; set; }
        public string IDNo { get; set; }
        public DateTime? IssueDate { get; set; } = DateTime.Now;
        public DateTime? ExpiryDate { get; set; } = DateTime.Now;
        public string Image { get; set; } 

    }
}
