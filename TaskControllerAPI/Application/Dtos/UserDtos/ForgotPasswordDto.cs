﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserDtos
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string ClientURI { get; set; }
    }
}
