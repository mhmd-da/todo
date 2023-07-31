using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Exceptions
{
    public class BaseException : ApplicationException
    {
        public ApiResponse Response { get; set; }

        public BaseException(string message, ApiResponse response)
            : base(message)
        {
            Response = response;
        }
    }
}
