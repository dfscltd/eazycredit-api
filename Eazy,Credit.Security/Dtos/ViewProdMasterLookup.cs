

namespace Eazy.Credit.Security.Dtos
{
    public class ViewProdMasterLookup
    {
        public string ProdCode { get; set; } = string.Empty;
        public string ProdName { get; set; } = string.Empty;
    }

    public class ViewCBNBankCodes
    {
        public string BankCode { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }

    public class ViewLookupResult
    {
        public string Code { get; set; } = string.Empty ;
        public string Description { get; set; } = string.Empty;
    }

    public class ViewNationalityLookup
    {
        public string CountryCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
    }

    public class ViewPrmCurrencyLookup
    {
        public string CurrCode { get; set; } = string.Empty;
        public string CurrDesc { get; set; } = string.Empty;
    }
}
