using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class AssignRoleToWorkflowLevelDto 
    {
        public string LevelID { get; set; } = string.Empty;
        public string WorkflowID { get; set; } = string.Empty;
        public string RoleID { get; set; } = string.Empty;
        //public string AddedBy { get; set; } = string.Empty;
        //public DateTime DateAdded { get; set; }
    }
}
