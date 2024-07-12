using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class ChangePasswordDto
    {
        public string Email { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string OtpCode { get; set; } = string.Empty; 
    }

    public class ChangePasswordResultDto
    {
        public bool IsPasswordChanged { get; set; } = false;
    }
}
