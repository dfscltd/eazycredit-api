using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EazyCoreObjs.Models
{
    [Table("NIPFTDirectCreditReq", Schema = "dbo")]
    public class NIPFTInwardDirectCreditReq
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SeqNo { get; set; }
        public string SessionID { get; set; }
        public string NameEnquiryRef { get; set; }
        public string DestinationInstitutionCode { get; set; }
        public int ChannelCode { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryBankVerificationNumber { get; set; }
        public string BeneficiaryKYCLevel { get; set; }
        public string OriginatorAccountName { get; set; }
        public string OriginatorAccountNumber { get; set; }
        public string OriginatorBankVerificationNumber { get; set; }
        public string OriginatorKYCLevel { get; set; }
        public string TransactionLocation { get; set; }
        public string Narration { get; set; }
        public string PaymentReference { get; set; }
        public decimal Amount { get; set; }
        public string ContraAccount { get; set; }
        public string EMTLevyAccount { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DateAdded { get; set; }
        public string TSQStatus { get; set; }
        public bool Reversed { get; set; }
    }
}
