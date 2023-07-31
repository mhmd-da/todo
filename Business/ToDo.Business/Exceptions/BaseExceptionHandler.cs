using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Exceptions
{
    public class BaseExceptionHandler
    {

        protected void NotFound(string message = "")
        {
            var response = new ApiResponse
            {
                Success = false,
                Status = HttpStatusCode.NotFound,
                Message = message
            };

            throw new NotFoundException(message, response);
        }


        protected void BadRequest(string message = "")
        {
            var response = new ApiResponse
            {
                Success = false,
                Status = HttpStatusCode.BadRequest,
                Message = message
            };

            throw new BadRequestException(message, response);
        }

        protected void UnAuthorized(string message = "")
        {
            var response = new ApiResponse
            {
                Success = false,
                Status = HttpStatusCode.Unauthorized,
                Message = message
            };

            throw new UnauthorizedException(message, response);
        }

        protected void Conflict(string message = "")
        {
            var response = new ApiResponse
            {
                Success = false,
                Status = HttpStatusCode.Conflict,
                Message = message
            };

            throw new ConflictException(message, response);
        }
    }
}
