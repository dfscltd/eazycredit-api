using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class ResetPasswordTokenDto
    {
        public string Email { get; set; } = string.Empty;
        public string TokenCode { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class ResetPasswordTokenResultDto
    {
        public bool IsPasswordReset { get; set; } = false;
    }
}
