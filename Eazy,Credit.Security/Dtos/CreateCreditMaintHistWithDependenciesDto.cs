using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateCreditMaintHistWithDependenciesDto
    {
        public string OperativeAccount { get; set; }
        public string AccountName { get; set; }
        public string FacilityDescription { get; set; }
        public string CreditType { get; set; }
        public string LoanProduct { get; set; }
        //public string CreditIDRef { get; set; }
        public string CreditFacilityType { get; set; }
        public decimal FacilityAmount { get; set; }
        public decimal BeneficiaryEquityContribution { get; set; }
        public DateTime DateOfApproval { get; set; }
        public DateTime EffectiveDate { get; set; }
        public short Tenor { get; set; }
        public string TenorType { get; set; }
        public decimal InterestRate { get; set; }
        //public bool IsRentLoan { get; set; }
        public string FacilityPurpose { get; set; }
        public string ScheduleType { get; set; }
        public string AddedBy { get; set; }
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
        public CreditScheduleParametersDto ScheduleParameters { get; set; } = new();
        public List<CreateCreditGuarantorsDto> CreditGuarantors { get; set; }
        public List<CreateCreditChargeDto> CreditCharges { get; set; }
        public List<CreateCreditSecuritiesDto> CreditSecurities { get; set; }

    }
}
