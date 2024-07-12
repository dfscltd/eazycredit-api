using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateCreditMaintHistDto
    {
        public string TransId { get; set; }
        public DateTime PostingDate { get; set; }
        public string CreditId { get; set; }
        public string Description { get; set; }
        public string CreditType { get; set; }
        public string OperativeAccountNo { get; set; }
        public bool Overdraft { get; set; }
        public string ProdCode { get; set; }
        public string AccountNo { get; set; }
        public string DisbursementAccountNo { get; set; }
        public decimal AmountGranted { get; set; }
        public decimal EquityContribution { get; set; }
        public DateTime DateApproved { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string TenorType { get; set; }
        public short Tenor { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Rate { get; set; }
        public decimal ArrearsRate { get; set; }
        public short BaseYear { get; set; }
        public string RepaymentType { get; set; }
        public string ConsentStatus { get; set; }
        public string PurposeType { get; set; }
        public string OwnershipId { get; set; }
        public string SecurityCoverageCode { get; set; }
        public string GuaranteeCoverageCode { get; set; }
        public string LegalActionCode { get; set; }
        public string CreditCardNo { get; set; }
        public string OnLendingStatus { get; set; }
        public string OnLendingCreditId { get; set; }
        public string Memo { get; set; }
        public bool ObserveLimit { get; set; }
        public bool? EditMode { get; set; }
        public string TransStatus { get; set; }
        public string BranchAdded { get; set; }
        public string AddedBy { get; set; }
        public string WorkstationAdded { get; set; }
        public string WorkstationIpadded { get; set; }
        public string BranchApproved { get; set; }
        public DateTime? PostingDateApproved { get; set; }
        public string AddedApprovedBy { get; set; }

        public string SegmentId { get; set; }
        public string FundSourceCode { get; set; }
        public string MisofficeCode { get; set; }
        public string ExtAccountNo { get; set; }
        public string AccountDesc { get; set; }
        public string PreferredRepaymentBankCBNCode { get; set; }
        public string PreferredRepaymentAccount { get; set; }
    }
}
