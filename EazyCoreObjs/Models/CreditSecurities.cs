using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("CreditSecurities", Schema = "dbo")]
    public class CreditSecurities
    {
        public long SeqNo { get; set; }
        public string CreditID { get; set; }
        public string SecurityType { get; set; }
        public string SecurityRefNo { get; set; }
        public decimal SecurityValue { get; set; }
        public string CurrCode { get; set; }
        public string ValuerType { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string Description { get; set; }
        public decimal ForcedSaleValue { get; set; }
        public DateTime MaturityDate { get; set; }
        public string SecurityAddress { get; set; }
        public string SecurityLocationCode { get; set; }
        public string SecurityTitleCode { get; set; }
    }
}
