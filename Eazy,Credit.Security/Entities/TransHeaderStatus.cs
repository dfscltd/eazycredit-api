using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class TransHeaderStatus
    {
        public string StatusCode {  get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
    }

    public class ActionTakenStatus
    {
        public string ActionCode { get; set; } = string.Empty;
        public string ActionStatus { get; set; } = string.Empty;
    }
}
