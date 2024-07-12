using System;
using System.Collections.Generic;
using System.Text;

namespace EazyCoreObjs.ViewModels
{
    public class ViewNameEnquirySingleRequest
    {
        public string AccountNo { get; set; }
        public string DestinationCode { get; set; }
        public int ChannelCode { get; set; }
    }
}
