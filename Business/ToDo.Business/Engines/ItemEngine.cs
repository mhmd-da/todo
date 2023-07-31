using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Requests.Item;
using ToDo.Common.Static;
using ToDo.Data.Contracts;
using ToDo.Data.Contracts.Repositories;

namespace ToDo.Business.Engines
{
    public class ItemEngine : BaseEngine, IItemEngine
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemEngine(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public List<Item> Get(GetItemRequest request)
        {
            var items = new List<Item>();

            if (!IsCategoryFound(request.CategoryId, request.UserId))
                NotFound(Messages.CategoryNotFound);

            items = _unitOfWork.ItemRepository.GetMany(i => (i.CategoryId == request.CategoryId) &&
                                                 (request.IsDone == null ? true : i.IsDone == request.IsDone) &&
                                                 (string.IsNullOrEmpty(request.Name) ? true : i.Name.Contains(request.Name))).ToList();


            return items;
        }

        public Item GetById(GetItemByIdRequest request)
        {
            Item item = null;

            if (!IsCategoryFound(request.CategoryId, request.UserId))
                NotFound(Messages.CategoryNotFound);

            item = _unitOfWork.ItemRepository.GetOne(i => (i.CategoryId == request.CategoryId) && (i.Id == request.Id));


            return item;
        }

        public Item Add(Item request, string userId)
        {
            Item item = null;

            if (!IsCategoryFound(request.CategoryId, userId))
                NotFound(Messages.CategoryNotFound);

            item = new Item()
            {
                Name = request.Name,
                IsDone = request.IsDone,
                Content = request.Content,
                CategoryId = request.CategoryId,
                DueDate = request.DueDate,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            _unitOfWork.ItemRepository.InsertOne(item);

            return item;
        }

        public Item Update(Item request, string userId)
        {
            Item item = null;

            if (!IsCategoryFound(request.CategoryId, userId))
                NotFound(Messages.CategoryNotFound);

            item = _unitOfWork.ItemRepository.GetOne(i => (i.CategoryId == request.CategoryId) && (i.Id == request.Id));

            if (item != null)
            {
                item.Name = request.Name;
                item.IsDone = request.IsDone;
                item.Content = request.Content;
                item.DueDate = request.DueDate;
                item.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.ItemRepository.ReplaceOne(item);
            }


            return item;
        }

        public Item Delete(DeleteItemRequest request)
        {
            Item item = null;

            if (!IsCategoryFound(request.CategoryId, request.UserId))
                NotFound(Messages.CategoryNotFound);

            item = _unitOfWork.ItemRepository.DeleteOne(i => (i.CategoryId == request.CategoryId) && (i.Id == request.Id));

            return item;
        }

        private bool IsCategoryFound(string categoryId, string userId)
        {
            var category = _unitOfWork.CategoryRepository.GetOne(c => (c.UserId == userId) && (c.Id == categoryId));
            return category != null;
        }
    }
}
