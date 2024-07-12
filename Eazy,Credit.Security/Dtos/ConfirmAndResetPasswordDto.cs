using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class ConfirmAndResetPasswordDto
    {
        public string OtpCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string NewPassword { get; set; } = string.Empty;
    }
}
