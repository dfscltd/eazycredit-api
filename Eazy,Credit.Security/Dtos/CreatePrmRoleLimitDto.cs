using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class CreatePrmRoleLimitDto
    {
        public string LimitId { get; set; } = string.Empty;
        public string LimitDesc { get; set; } = string.Empty;
        public decimal? LowerLimit { get; set; } = decimal.Zero;
        public decimal? UpperLimit { get; set; } = decimal.Zero;
        public decimal? CummulativeLimit { get; set; } = decimal.Zero;
    }


    public class RolesPrmLimitDto
    {
        public string RoleID {  get; set; }= string.Empty;
        public string RoleName { get; set; }=string.Empty;
        public string LimitId { get; set; } = string.Empty;
        public string LimitDesc { get; set; } = string.Empty;
        public decimal? LowerLimit { get; set; } = decimal.Zero;
        public decimal? UpperLimit { get; set; } = decimal.Zero;
        public decimal? CummulativeLimit { get; set; } = decimal.Zero;
    }
}
