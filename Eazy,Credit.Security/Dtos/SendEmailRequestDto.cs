using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class SendEmailRequestDto
    {
        public string DestEmail {  get; set; }
        public string Subject {  get; set; }
        public string Message {  get; set; }
    }
}
