using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using ToDo.Business.Contracts.Services;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Client.Entities.Responses;

namespace ToDo.API.Controllers
{
    [Route("api/todo/category")]
    [ApiController]
    [Authorize(Roles = "NormalUser")]
    public class CategoryController : BaseController
    {
        #region Fields 

        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        public CategoryController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        }

        #endregion Constructor

        [HttpGet]
        public ActionResult<ApiResponse> Get([FromQuery] GetCategoryRequest request)
        {
            request.UserId = GetUserId();
            return ExecuteOperation(() => _categoryService.Get(request));
        }

        [HttpGet("{Id}")]
        public ActionResult<ApiResponse> GetById([FromRoute] string id, [FromQuery] GetCategoryByIdRequest request)
        {
            request.UserId = GetUserId();
            request.Id = id;
            return ExecuteOperation(() => _categoryService.GetById(request));
        }

        [HttpPost]
        public ActionResult<ApiResponse> Add([FromBody] AddCategoryRequest request)
        {
            request.UserId = GetUserId();
            return ExecuteOperation(() => _categoryService.Add(request));
        }

        [HttpPut("{id}")]
        public ActionResult<ApiResponse> Update([FromRoute] string id, [FromBody] UpdateCategoryRequest request)
        {
            request.UserId = GetUserId();
            request.Id = id;
            return ExecuteOperation(() => _categoryService.Update(request));
        }

        [HttpDelete("{Id}")]
        public ActionResult<ApiResponse> Delete([FromRoute] string id, [FromQuery] DeleteCategoryRequest request)
        {
            request.UserId = GetUserId();
            request.Id = id;
            return ExecuteOperation(() => _categoryService.Delete(request));
        }
    }
}
