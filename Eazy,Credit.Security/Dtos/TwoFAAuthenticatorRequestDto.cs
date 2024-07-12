using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class TwoFAAuthenticatorRequestDto
    {
        public string TokenCode { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
        public bool RememberMachine { get; set; }
    }
}
