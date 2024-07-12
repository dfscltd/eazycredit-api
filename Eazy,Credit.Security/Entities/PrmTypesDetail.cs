using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class PrmTypesDetail
    {
        public short TypeCode { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
