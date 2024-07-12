using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("LedgerChart", Schema = "dbo")]
    public class LedgerChart
    {
        public string LedgerChartCode { get; set; }
        public string LedgerChartDesc { get; set; }
        public string LedgerGroupCode { get; set; }
        public string LedgerAttrCode { get; set; }
        public string CurrCode { get; set; }
        public bool HeadOfficeOnly { get; set; }
        public bool IFRS { get; set; }
        public bool EditMode { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public string AddedBy { get; set; }
        public string AddedApprovedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastApprovedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateAddedApproved { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateLastApproved { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan? TimeAddedApproved { get; set; }
        public TimeSpan? TimeLastModified { get; set; }
        public TimeSpan? TimeLastApproved { get; set; }
        public bool Revalue { get; set; }
        public string BizUnitCode { get; set; }
    }
}
