﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message, ApiResponse response)
                    : base(message, response)
        {
            Response = response;
        }
    }
}
