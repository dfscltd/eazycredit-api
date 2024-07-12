using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateUserDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? OtherName { get; set; } = string.Empty;
        public string? ShortName { get; set; } = string.Empty;
        public bool Disabled {  get; set; } = false;
        public string? DisableReason {  get; set; }
        public DateTime? EnableDate {  get; set; }
        public string? EmployeeId {  get; set; }
        public bool IsAdmin {  get; set; } = false;
        //DateTime? PreviousLogin;
        //DateTime? PreviousMobileLogin;
        //int? SuccessfulLoginsToday;
        //DateTime? PreviousPasswordChange;
        //DateTime? PasswordExpiration;
        //int? LogInRetries;
        public byte[]? UserPhoto { get; set; } = null;
        //bool? EnforcePasswordReset;
        //DateTime DateAdded;
        public string AddedBy { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        //DateTime? DateLastModified;
        //string? LastModifiedBy;
        //string? ContentType;
        //DateTime? LastLoginDate;
    }
}
