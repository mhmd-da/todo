using Microsoft.Extensions.DependencyInjection;
using System;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Contracts.Services;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly IAdminEngine _adminEngine;
        public AdminService(IServiceProvider serviceProvider):base(serviceProvider)
        {
            _adminEngine = serviceProvider.GetRequiredService<IAdminEngine>();
        }

        public ApiResponse Get()
        {
            var result = _adminEngine.Get();

            return Success(result);
        }
    }
}
