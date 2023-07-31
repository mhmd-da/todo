using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Web;
using ToDo.Business.Contracts.Engines;
using ToDo.Client.Entities.Requests.Item;
using ToDo.Client.Entities.Responses;

namespace ToDo.API.Controllers
{
    [Route("api/todo/category")]
    [ApiController]
    [Authorize(Roles = "NormalUser")]
    public class ItemController : BaseController
    {
        #region Fields 

        private readonly IItemService _itemService;

        #endregion

        #region Constructor

        public ItemController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _itemService = serviceProvider.GetRequiredService<IItemService>();
        }

        #endregion Constructor

        [HttpGet("{categoryId}/item")]
        public ActionResult<ApiResponse> Get([FromRoute] string categoryId, [FromQuery] GetItemRequest request)
        {
            request.UserId = GetUserId();
            request.CategoryId = categoryId;
            return ExecuteOperation(() => _itemService.Get(request));
        }

        [HttpGet("{categoryId}/item/{id}")]
        public ActionResult<ApiResponse> GetById([FromRoute] string categoryId, [FromRoute] string id, [FromQuery] GetItemByIdRequest request)
        {
            request.UserId = GetUserId();
            request.CategoryId = categoryId;
            request.Id = id;
            return ExecuteOperation(() => _itemService.GetById(request));
        }

        [HttpPost("{categoryId}/item")]
        public ActionResult<ApiResponse> Add([FromRoute] string categoryId, [FromBody] AddItemRequest request)
        {
            request.UserId = GetUserId();
            request.CategoryId = categoryId;
            return ExecuteOperation(() => _itemService.Add(request));
        }

        [HttpPut("{categoryId}/item/{id}")]
        public ActionResult<ApiResponse> Update([FromRoute] string categoryId, [FromRoute] string id, [FromBody] UpdateItemRequest request)
        {
            request.UserId = GetUserId();
            request.CategoryId = categoryId;
            request.Id = id;
            return ExecuteOperation(() => _itemService.Update(request));
        }

        [HttpDelete("{categoryId}/item/{id}")]
        public ActionResult<ApiResponse> Delete([FromRoute] string categoryId, [FromRoute] string id, [FromQuery] DeleteItemRequest request)
        {
            request.UserId = GetUserId();
            request.CategoryId = categoryId;
            request.Id = id;
            return ExecuteOperation(() => _itemService.Delete(request));
        }
    }
}
