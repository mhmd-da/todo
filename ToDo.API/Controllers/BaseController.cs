using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Exceptions;
using ToDo.Client.Entities.Requests.Item;
using ToDo.Client.Entities.Responses;
using ToDo.Common.Static;

namespace ToDo.API.Controllers
{
    public class BaseController : ControllerBase
    {

        protected readonly IHttpContextAccessor _httpContextAccessor;

        #region Constructor

        public BaseController(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        #endregion Constructor

        protected ActionResult<ApiResponse> ExecuteOperation(Func<ApiResponse> codetoExecute)
        {
            ApiResponse response;
            try
            {
                response = codetoExecute();
            }
            catch (BaseException ex)
            {
                SetStatusCode(ex.Response.Status);
                response = ex.Response;
            }
            catch (Exception ex)
            {
                SetStatusCode(HttpStatusCode.InternalServerError);
                response = new ApiResponse { Message = ex.Message, Status = HttpStatusCode.InternalServerError, Success = false };
            }

            return StatusCode(HttpContext.Response.StatusCode, response);
        }


        protected void SetStatusCode(HttpStatusCode code)
        {
            HttpContext.Response.StatusCode = (int)code;
        }

        protected string GetUserId()
        {
            string result = null;

            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            if (claimsPrincipal != null)
            {
                Claim claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.UserId);
                result = claim?.Value;
            }

            return result;
        }
    }
}
