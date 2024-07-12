using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateWorkflowsDto
    {
        public string WorkflowID { get; set; } = string.Empty;
        public string WorkflowTitle { get; set; } = string.Empty;
        public string WorkflowNotes { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
    }


    public class ResultWorkflowsDto
    {
        public string WorkflowID { get; set; } = string.Empty;
        public string WorkflowTitle { get; set; } = string.Empty;
        public string WorkflowNotes { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }
    }
}
