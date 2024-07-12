using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class ResultRoleToWorkflowLevelDto
    {
        public string LevelID { get; set; } = string.Empty;
        public string WorkflowLevelTitle { get; set; } = string.Empty;
        public string WorkflowID { get; set; } = string.Empty;
        public string WorkflowTitle { get; set; } = string.Empty;
        public int LevelOrder { get; set; }
        public int Previous { get; set; } = 0;
        public int Next { get; set; }
        public string RoleID { get; set; } = string.Empty;
        public string RoleName {  get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
    }
}
