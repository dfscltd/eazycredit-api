using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class LoanApplicationRequestDto
    {
        public string OperativeAccount { get; set; }
        public string AccountName { get; set; }
        public string FacilityDescription { get; set; }
        public string CreditType { get; set; }
        public string LoanProduct { get; set; }
        public decimal FacilityAmount { get; set; }
        public decimal BeneficiaryEquityContribution { get; set; }
        public DateTime DateOfApproval { get; set; }
        public DateTime EffectiveDate { get; set; }
        public short Tenor { get; set; }
        public string TenorType { get; set; }
        public decimal InterestRate { get; set; }
        public string FacilityPurpose {  get; set; }
        public string ScheduleType { get; set; }
        public string Workflow { get; set; }
        public string AddedBy { get; set; }
        public CreditScheduleParametersDto ScheduleParameters { get; set; } = new();
        public string PreferredRepaymentBankCBNCode { get; set; }
        public string PreferredRepaymentAccount { get; set; }

    }

    public class CreditUpdateRequestDto
    {
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public string OperativeAccount { get; set; }
        public string AccountName { get; set; }
        public string FacilityDescription { get; set; }
        public string CreditType { get; set; }
        public string LoanProduct { get; set; }
        public decimal FacilityAmount { get; set; }
        public decimal BeneficiaryEquityContribution { get; set; }
        public DateTime DateOfApproval { get; set; }
        public DateTime EffectiveDate { get; set; }
        public short Tenor { get; set; }
        public string TenorType { get; set; }
        public decimal InterestRate { get; set; }
        public string ScheduleType { get; set; }
        public string Workflow { get; set; }
        public string AddedBy { get; set; }
        public string PreferredRepaymentBankCBNCode { get; set; }
        public string PreferredRepaymentAccount { get; set; }

    }

}
