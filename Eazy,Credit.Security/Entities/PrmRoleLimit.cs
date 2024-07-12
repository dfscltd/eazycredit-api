using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class PrmRoleLimit
    {
        public string LimitId {  get; set; } = string.Empty;
        public string LimitDesc { get; set; } = string.Empty ;
        public decimal? LowerLimit { get; set; } = decimal.Zero;
        public decimal? UpperLimit { get; set; } = decimal.Zero;
        public decimal? CummulativeLimit { get; set; } = decimal.Zero;
    }

    public class AssignRoleToLimit : CommonFields
    {
        public string LimitId { get; set; } = string.Empty;
        public string UserRoleID { get; set; } = string.Empty;
    }
}
