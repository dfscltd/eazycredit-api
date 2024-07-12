using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class VwAccEmailAddr
    {
        public long Sequence { get; set; }
        public string AccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string EmailAddr { get; set; }
        public string Name { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
    }
}
