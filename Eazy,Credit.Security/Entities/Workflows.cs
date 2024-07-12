using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class Workflows: CommonFields
    {
        public string WorkflowID {  get; set; } = string.Empty;
        public string WorkflowTitle { get; set; } = string.Empty;
        public string WorkflowNotes { get; set; } = string.Empty;
        //public DateTime DateAdded {  get; set; }
        //public DateTime? DateLastModified {  get; set; } 
        //public string AddedBy { get; set; } = string.Empty;
        //public string? LastModifiedBy {  get; set; }
    }
}
