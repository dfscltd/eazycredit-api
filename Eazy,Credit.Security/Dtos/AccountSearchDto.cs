using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class AccountSearchDto
    {
        public string AccountNo { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty ;
        public string AccountName { get; set; } = string.Empty;
        public string CusNo { get; set; } = string.Empty;
    }
}
