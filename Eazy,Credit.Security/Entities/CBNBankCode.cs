using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class CBNBankCode
    {
        public string CBNCode { get; set; } = string.Empty;
        public string InstitutionName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string SortCode { get; set; } = string.Empty ;
        public string NIPBankCode { get; set; } = string.Empty;
    }
}
