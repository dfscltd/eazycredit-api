using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class loginModelDTO
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool LockoutOnFailure { get; set; }
    }
}
