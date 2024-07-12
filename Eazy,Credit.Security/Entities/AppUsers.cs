using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class AppUsers : IdentityUser<string>
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string OtherName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public int LogInRetries { get; set; }
        public byte[]? UserPhoto { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public string AuthCode { get; set; } = string.Empty;
        public string? EmployeeID { get; set; } = string.Empty;
        public DateTime? PasswordExpiration { get; set; }
        public bool PasswordReset { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string AddedBy { get; set; } = string.Empty;

        public bool Disabled { get; set; } = false;
        public string? DisableReason { get; set; }
        public DateTime? EnableDate { get; set; }
        //public bool Locked { get; set; }
        public int? SuccessfulLoginsToday { get; set; } = 0;
        public string PasswordResetCode { get; set; } = string.Empty;
        public string PasswordChangeCode { get; set; } = string.Empty;
    }
}
