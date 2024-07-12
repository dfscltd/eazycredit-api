using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class ViewAPIResponse<T> where T : class
    {
        public string ResponseCode {  get; set; }
        public string ResponseMessage {  get; set; }
        public T ResponseResult {  get; set; }
    }
}
