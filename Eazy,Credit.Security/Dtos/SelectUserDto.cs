using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class SelectUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string OtherName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public bool Disabled { get; set; } = false;
        public string? DisableReason { get; set; }
        public DateTime? EnableDate { get; set; }
        //public bool Locked { get; set; }
        public string? EmployeeId { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime? PreviousLogin { get; set; }
        public int? SuccessfulLoginsToday { get; set; }
        public DateTime? PreviousPasswordChange { get; set; }
        public DateTime? PasswordExpiration { get; set; }
        public int? LogInRetries { get; set; }
        public byte[]? UserPhoto { get; set; } = null;
        public bool TwoFactorEnabled { get; set; }
        //public DateTime DateAdded {  get; set; }
        public string AddedBy { get; set; } = string.Empty;

    }
}
