using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class PrmNumberSequence
    {
        public int NumberId { get; set; }
        public string Description { get; set; }
        public int NextNumber { get; set; }
        public byte NumberLength { get; set; }
        public string Separator { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool Lockup { get; set; }

    }
}
