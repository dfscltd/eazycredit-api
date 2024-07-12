using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateWorkflowLevelDto
    {
        public string LevelID { get; set; } = string.Empty;
        public string WorkflowID { get; set; } = string.Empty;
        public string WorkflowLevelTitle { get; set; } = string.Empty;
        public bool AllowDocUpload { get; set; }
        public bool FinalLevel { get; set; }
        public int LevelOrder { get; set; } = 0;
        public string AddedBy { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
    }

    public class ResulyWorkflowLevelDto
    {
        public string LevelID { get; set; } = string.Empty;
        public string WorkflowID { get; set; } = string.Empty;
        public string WorkflowTitle { get; set; } = string.Empty;
        public string WorkflowLevelTitle { get; set; } = string.Empty;
        public bool AllowDocUpload { get; set; }
        public bool FinalLevel { get; set; }
        public int LevelOrder { get; set; } = 0;
        public int Previous { get; set; } = 0;
        public int Next { get; set; }
        public string AddedBy { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
    }
}
