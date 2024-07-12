using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class SendGridOptionsDto
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string SenderName { get; set; }
    }
}
