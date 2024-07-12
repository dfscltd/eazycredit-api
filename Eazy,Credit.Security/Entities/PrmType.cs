using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class PrmType
    {
        public short TypeCode { get; set; }
        public string TypeDesc { get; set; }
        public bool UserDefined { get; set; }
        public string LabelCode { get; set; }
        public string LabelDesc { get; set; }
    }
}
