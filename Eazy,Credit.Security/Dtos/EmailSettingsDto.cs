using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class EmailSettingsDto
    {
        public int Port { get; set; } = 0;
        public string? Host { get; set; } = string.Empty;
        public string? FromEmail { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
    }
}
