using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class ViewLookupResponse
    {
        public List<ViewProdMasterLookup> loanProduct { get; set; } = new List<ViewProdMasterLookup>();
        public List<ViewCBNBankCodes> preferredRepaymentBankCBNCode { get; set; } = new List<ViewCBNBankCodes>();
        public List<ViewLookupResult> creditType { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> facilityPurpose { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> tenorType { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> scheduleType { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> principalRepaymentFreq { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> interestRepaymentFreq { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> principalMoratFreq { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> interestMoratFreq { get; set; } = new List<ViewLookupResult>();
    }

    public class ViewGuarantorLookupResponse
    {
        public List<ViewLookupResult> guarantorType { get; set; } = new List<ViewLookupResult>();
        public List<ViewNationalityLookup> nationality { get; set; } = new List<ViewNationalityLookup>();
        public List<ViewLookupResult>? gender { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult>? maritalStatus { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> legalConstitution { get; set; } = new List<ViewLookupResult>();
    }

    public class ViewSecurityLookupResponse
    {
        public List<ViewLookupResult> securityType { get; set; } = new List<ViewLookupResult>();
        public List<ViewPrmCurrencyLookup> currCode { get; set; } = new List<ViewPrmCurrencyLookup>();
        public List<ViewLookupResult> valuerType { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> securityLocationCode { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> securityTitleCode { get; set; } = new List<ViewLookupResult>();
    }

    public class ViewChargesLookupResponse
    {
        public List<ViewLookupResult> chargeBaseCode { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> rateType { get; set; } = new List<ViewLookupResult>();
        public List<ViewLookupResult> freqCode { get; set; } = new List<ViewLookupResult>();
    }
}
