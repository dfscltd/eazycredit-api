using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("CusRelParties", Schema = "dbo")]
    public class CusRelParties
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Sequence { get; set; } 
        public string CusID { get; set; }
        public string CusRelTypeCode { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string CountryCode { get; set; }
        public string LocalGovtCode { get; set; }
        public string StateCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string EmployStatus { get; set; }
        public string OccCode { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public string AddedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime PostingDateLastModified { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string BvnNo { get; set; }
        public bool PEP { get; set; }
    }
}
