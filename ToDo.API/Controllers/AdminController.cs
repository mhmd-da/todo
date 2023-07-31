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
    [Route("admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        #region Fields 

        private readonly IAdminService _adminService;

        #endregion

        #region Constructor

        public AdminController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _adminService = serviceProvider.GetRequiredService<IAdminService>();
        }

        #endregion Constructor

        [HttpGet]
        public ActionResult<ApiResponse> Get()
        {
            return ExecuteOperation(() => _adminService.Get());
        }
    }
}
