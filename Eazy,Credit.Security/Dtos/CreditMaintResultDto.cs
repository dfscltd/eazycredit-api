using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreditMaintResultDto
    {
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string OperativeAccount { get; set; }
        public string AccountName { get; set; }
        public string FacilityDescription { get; set; }
        public string CreditType { get; set; }
        public string CreditTypeDesc {  get; set; }
        public string LoanProduct { get; set; }
        public string ProductName { get; set; }
        public string PurposeType { get; set; }
        public string PurposeTypeDesc { get; set; }
        //public string CreditFacilityTypeDesc { get; set; }
        public decimal FacilityAmount { get; set; }
        public decimal BeneficiaryEquityContribution { get; set; }
        public DateTime DateOfApproval { get; set; }
        public DateTime EffectiveDate { get; set; }
        public short Tenor { get; set; }
        public string TenorType { get; set; }
        public string TenorTypeDesc { get; set; }
        public decimal InterestRate { get; set; }
        //public bool IsRentLoan { get; set; }
        public string RepaymentType { get; set; }
        public string RepaymentTypeDesc { get; set; }
        //public DateTime PrincipalRepaymentStartDate { get; set; }
        //public DateTime InterestRepaymentStartDate { get; set; }
        //public string PrincipalRepaymentFreq { get; set; }
        //public string InterestRepaymentFreq { get; set; }
        //public short PrincipalMorat { get; set; }
        //public string PrincipalMoratFreq { get; set; }
        //public short InterestMorat { get; set; }
        //public string InterestMoratFreq { get; set; }
        //public string AccountName { get; set; }
        public string PreferredRepaymentBankCBNCode { get; set; }
        public string PreferredRepaymentAccount { get; set; }
        public List<CreditGuarantorsResultDto> CreditGuarantors { get; set; }
        public List<CreditChargeResultDto> CreditCharges { get; set; }
        public List<CreditSecuritiesResultDto> CreditSecurities { get; set; }
        public CreditScheduleParametersResultDto ScheduleParameters { get; set; }
        public List<CreditScheduleSummaryDto> ScheduleLines { get; set; }

    }

    public class CreditGuarantorsResultDto
    {
        public long SeqNo { get; set; }
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string GuarantorType { get; set; }
        public string GuarantorTypeDesc { get; set; }
        public string GuarantorFullNames { get; set; }
        public string Nationality { get; set; }
        public string MainBusiness { get; set; }
        public string BusinessRegNo { get; set; }
        public string CertIncorp { get; set; }
        public string BusinessTaxNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string GenderDesc { get; set; }
        public string MaritalStatus { get; set; }
        public string MaritalStatusDesc { get; set; }
        public string LegalConstitution { get; set; }
        //public string AddedBy { get; set; }
        //public DateTime PostingDateAdded { get; set; }
        public string Address { get; set; }
        public string TelNo { get; set; }
        public string EmailAddr { get; set; }
        public decimal Liability { get; set; }
        public string BvnNo { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public bool Pep { get; set; }

    }

    public class CreditChargeResultDto
    {
        public long Sequence { get; set; }
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string ChargeBaseCode { get; set; }
        public string ChargeBaseDesc { get; set; }
        public decimal Rate { get; set; }
        public string RateType { get; set; }
        public string RateDesc { get; set; }
        public string FreqCode { get; set; }
        public string FreqDesc { get; set; }
        public DateTime NextExecDate { get; set; }
        public bool? Upfront { get; set; }
        //public string AddedBy { get; set; }
        //public DateTime PostingDateAdded { get; set; }
    }

    public class CreditSecuritiesResultDto
    {
        public long SeqNo { get; set; }
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string SecurityType { get; set; }
        public string SecurityDesc { get; set; }
        public string SecurityRefNo { get; set; }
        public decimal SecurityValue { get; set; }
        public string CurrCode { get; set; }
        public string ValuerType { get; set; }
        public string ValuerDesc { get; set; }
        //public string AddedBy { get; set; }
        //public DateTime PostingDateAdded { get; set; }
        public string Description { get; set; }
        public decimal ForcedSaleValue { get; set; }
        public DateTime? MaturityDate { get; set; }
        public string SecurityAddress { get; set; }
        public string SecurityLocationCode { get; set; }
        public string SecurityLocation { get; set; }
        public string SecurityTitleCode { get; set; }
        public string SecurityTitle { get; set; }

    }

    public class CreditScheduleParametersResultDto
    {
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public DateTime PrincipalRepaymentStartDate { get; set; }
        public DateTime InterestRepaymentStartDate { get; set; }
        public string PrincipalRepaymentFreq { get; set; }
        public string InterestRepaymentFreq { get; set; }
        public short PrincipalMorat { get; set; }
        public string PrincipalMoratFreq { get; set; }
        public short InterestMorat { get; set; }
        public string InterestMoratFreq { get; set; }
    }
}
