using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class ApprovingUsersRequestDto
    {
        public string WorkflowId { get; set; }
        public string LevelId { get; set; }
        public string CreditId {  get; set; }
    }
}
