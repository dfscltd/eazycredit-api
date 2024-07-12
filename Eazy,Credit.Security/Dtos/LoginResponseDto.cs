using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class LoginResponseDto
    {
        public bool Authenticated {  get; set; } = false;
        public string Token { get; set; }
        public bool TwoFactorEnabled {  get; set; }
        public string UserId {  get; set; }
        public string UserName {  get; set; }
        public string Email {  get; set; }
        public bool Enabled {  get; set; }
        public bool Locked {  get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? LastPasswordChangedDate {  get; set; }
        public DateTime? PasswordExpiration { get; set; }
        public bool PasswordReset {  get; set; }
        public List<string> Branches {  get; set; }
        public bool PasswordExired { get; set; }
    }
}
