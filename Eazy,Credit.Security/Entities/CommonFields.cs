using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public abstract class CommonFields
    {
        public DateTime DateAdded { get; set; }
        public DateTime? DateLastModified { get; set; }
        public string AddedBy { get; set; } = string.Empty;
        public string LastModifiedBy { get; set; } = string.Empty;
    }
}
