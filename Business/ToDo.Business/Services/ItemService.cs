using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Requests.Item;
using ToDo.Client.Entities.Responses;
using ToDo.Client.Entities.Responses.Item;
using ToDo.Common.Static;

namespace ToDo.Business.Services
{
    public class ItemService : BaseService, IItemService
    {
        private readonly IItemEngine _itemEngine;

        public ItemService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _itemEngine = serviceProvider.GetRequiredService<IItemEngine>();
        }

        public ApiResponse Get(GetItemRequest request)
        {
            List<Item> items = _itemEngine.Get(request);

            if (items == null || items.Count == 0)
                NotFound(string.Format(Messages.ItemsNotFound, request.CategoryId));

            List<GetItemResponse> response = _autoMapperLoader.Mapper.Map<List<GetItemResponse>>(items);

            return Success(response);
        }

        public ApiResponse GetById(GetItemByIdRequest request)
        {
            Item item = _itemEngine.GetById(request);

            if (item == null)
                NotFound(string.Format(Messages.ItemNotFound, request.Id, request.CategoryId));

            GetItemResponse response = _autoMapperLoader.Mapper.Map<GetItemResponse>(item);

            return Success(response);
        }

        public ApiResponse Add(AddItemRequest request)
        {
            Item item = _autoMapperLoader.Mapper.Map<Item>(request);

            item = _itemEngine.Add(item, request.UserId);

            if (item == null)
                BadRequest(Messages.CreateItemError);

            GetItemResponse response = _autoMapperLoader.Mapper.Map<GetItemResponse>(item);

            return Created(response);
        }

        public ApiResponse Update(UpdateItemRequest request)
        {
            Item item = _autoMapperLoader.Mapper.Map<Item>(request);

            item = _itemEngine.Update(item, request.UserId);

            if (item == null)
                BadRequest(Messages.UpdateItemError);

            GetItemResponse response = _autoMapperLoader.Mapper.Map<GetItemResponse>(item);

            return Success(response);
        }

        public ApiResponse Delete(DeleteItemRequest request)
        {
            Item item = _itemEngine.Delete(request);

            if (item == null)
                BadRequest(Messages.DeleteItemError);

            GetItemResponse response = _autoMapperLoader.Mapper.Map<GetItemResponse>(item);

            return Success(response);
        }
    }
}
