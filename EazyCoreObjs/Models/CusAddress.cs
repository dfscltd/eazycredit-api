using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("EntAddress", Schema = "dbo")]
    public class CusAddress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequence { get; set; }
        public string EntityID { get; set; }
        public string EntityTypeCode { get; set; }
        public string AddrTypeCode { get; set; }
        public string Street { get; set; }
        public string CountryCode { get; set; }
        public string StateCode { get; set; }
        public string LocalGovtCode { get; set; }
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
