using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreateTransHeaderStageDto
    {
        public string CreditID { get; set; } = string.Empty; //this should be the same with the transId on the CAM
        public string TransDesc { get; set; } = string.Empty; //This should be the same with loan description
        public string TransStatus { get; set; } = string.Empty;
        public string Workflow { get; set; } = string.Empty;
        public string Workflowlevel { get; set; } = string.Empty;
        public string TransComment { get; set; } = string.Empty;
        public string ActionCode { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }

    public class TransHeaderStageResultDto
    {
        public string CreditID { get; set; } = string.Empty; //this should be the same with the transId on the CAM
        public string TransDesc { get; set; } = string.Empty; //This should be the same with loan description
        public string TransCode { get; set; } = string.Empty;
        public string TransStatus { get; set; } = string.Empty;
        public string Workflow { get; set; } = string.Empty;
        public string Workflowlevel { get; set; } = string.Empty;
        public string TransComment { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ActionCode {  get; set; } = string.Empty;
        public string ActionStatus { get; set; } = string.Empty ;
    }
}
