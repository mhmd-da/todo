using Microsoft.Extensions.DependencyInjection;
using System;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Contracts.Services;
using ToDo.Client.Entities.Requests.Auth;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IAuthEngine _authEngine;
        public AuthService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _authEngine = serviceProvider.GetRequiredService<IAuthEngine>();
        }

        public ApiResponse CreateToken(TokenRequest request)
        {
            var tokenResponse = _authEngine.CreateToken(request);

            return Success(tokenResponse);
        }
    }
}
