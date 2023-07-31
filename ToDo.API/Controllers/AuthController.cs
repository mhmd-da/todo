using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Contracts.Services;
using ToDo.Client.Entities.Requests.Auth;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Client.Entities.Responses;
using ToDo.Client.Entities.Responses.Auth;
using ToDo.Client.Entities.Responses.Category;

namespace ToDo.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    //[Authorize(Roles = "SuperAdmin")]
    public class AuthController : BaseController
    {
        #region Fields 

        private readonly IAuthService _authService;

        #endregion

        #region Constructor

        public AuthController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _authService = serviceProvider.GetRequiredService<IAuthService>();
        }

        #endregion Constructor

        [HttpPost]
        public ActionResult<ApiResponse> CreateToken([FromBody] TokenRequest request)
        {
            return ExecuteOperation(() => _authService.CreateToken(request));
        }
    }
}
