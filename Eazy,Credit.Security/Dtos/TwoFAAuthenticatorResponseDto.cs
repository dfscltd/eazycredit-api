using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class TwoFAAuthenticatorResponseDto
    {
        public bool IsTokenSent {  get; set; }
        public string Token { get; set; }
    }
}
