using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class RolesMembershipDto
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty ;
        public string RoleDescription { get; set; } = string.Empty;

    }
}
