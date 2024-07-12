﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazy.Credit.Security.Dtos
{
    public class TwoFAAuthenticatorSignInResultDto
    {
        public bool Validated { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}