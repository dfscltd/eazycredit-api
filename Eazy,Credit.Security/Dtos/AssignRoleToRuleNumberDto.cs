using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class AssignRoleToRuleNumberDto
    {
        public string LimitId { get; set; } = string.Empty;
        public string UserRoleID { get; set; } = string.Empty;
    }
}
