using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, ApiResponse response)
                    : base(message, response)
        {
            Response = response;
        }
    }
}
