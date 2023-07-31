using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using ToDo.Business.AutoMapper;
using ToDo.Business.Exceptions;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Services
{
    public class BaseService : BaseExceptionHandler
    {
        protected readonly AutoMapperLoader _autoMapperLoader;

        public BaseService(IServiceProvider serviceProvider)
        {
            _autoMapperLoader = serviceProvider.GetRequiredService<AutoMapperLoader>();
        }

        protected ApiResponse Success(object data = null)
        {
            return new ApiResponse
            {
                Success = true,
                Status = HttpStatusCode.OK,
                Message = "Success",
                Data = data
            };
        }

        protected ApiResponse Created(object data = null)
        {
            return new ApiResponse
            {
                Success = true,
                Status = HttpStatusCode.Created,
                Message = "Success",
                Data = data
            };
        }
    }
}
