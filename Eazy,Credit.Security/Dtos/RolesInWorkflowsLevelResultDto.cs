using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class RolesInWorkflowsLevelResultDto
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string WorkflowsLevelDesc { get; set; } = string.Empty ;
        public string WorkflowLevelId { get; set; }= string.Empty;
        public string WorkflowId { get; set; } = string.Empty;
        public string WorkflowName { get; set; } = string.Empty;
    }
}
