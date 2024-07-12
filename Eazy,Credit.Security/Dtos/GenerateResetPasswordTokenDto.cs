using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class GenerateResetPasswordTokenDto
    {
        public string email { get; set; } = string.Empty;
    }

    public class GenerateResetPasswordTokenResultDto
    {
        public bool IsTokenSent { get; set; } = false;
        public string Token { get; set; }
    }
}
