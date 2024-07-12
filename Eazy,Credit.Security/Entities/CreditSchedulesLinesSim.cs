﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Entities
{
    public class CreditSchedulesLinesSim
    {
        public long Sequence { get; set; }
        public string TransId { get; set; }
        public string CreditId { get; set; }
        public DateTime ValueDate { get; set; }
        public string LineType { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string ChargeBaseCode { get; set; }
    }
}