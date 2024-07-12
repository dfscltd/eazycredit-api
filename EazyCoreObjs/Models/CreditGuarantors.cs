using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("CreditGuarantors", Schema = "dbo")]
    public class CreditGuarantors
    {
        public long SeqNo { get; set; }
        public string CreditID { get; set; }
        public string GuarantorType { get; set; }
        public string GuarantorFullNames { get; set; }
        public string Nationality { get; set; }
        public string MainBusiness { get; set; }
        public string BusinessRegNo { get; set; }
        public string CertIncorp { get; set; }
        public string BusinessTaxNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string LegalConstitution { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime PostingDateAdded { get; set; }
        public TimeSpan TimeAdded { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime DateLastModified { get; set; }
        public TimeSpan TimeLastModified { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string EmailAddr { get; set; }
        public decimal Liability { get; set; }
        public string BvnNo { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public bool PEP { get; set; }

    }
}
