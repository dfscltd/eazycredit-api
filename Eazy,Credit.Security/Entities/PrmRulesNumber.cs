using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class PrmRulesNumber
    {
        public short Parameter { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string ModuleCode { get; set; } = string.Empty;
    }
}
