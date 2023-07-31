using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Common.Enums;
using ToDo.Common.Static;
using ToDo.Data.Contracts;
using ToDo.Data.Contracts.Repositories;

namespace ToDo.Business.Engines
{
    public class CategoryEngine : BaseEngine, ICategoryEngine
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryEngine(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public List<Category> Get(GetCategoryRequest request)
        {
            var categories = _unitOfWork.CategoryRepository.GetMany(c => (c.UserId == request.UserId) &&
                                                              (request.Label == null ? true : c.Label == request.Label) &&
                                                              (request.IsStarred == null ? true : c.IsStarred == request.IsStarred) &&
                                                              (string.IsNullOrEmpty(request.Name) ? true : c.Name.Contains(request.Name)));

            int offset = request.GetPageSize() * (request.GetPageIndex() - 1);
            var result = categories.Skip(offset).Take(request.GetPageSize()).ToList();

            return result;
        }

        public Category GetById(GetCategoryByIdRequest request)
        {
            var category = _unitOfWork.CategoryRepository.GetOne(c => (c.UserId == request.UserId) && (c.Id == request.Id));

            return category;
        }

        public Category Add(Category request)
        {
            if (string.IsNullOrEmpty(request.Name))
                BadRequest(Messages.NameRequired);

            Category category = new Category()
            {
                Name = request.Name,
                Color = request.Color,
                Label = request.Label ?? Label.Default,
                IsStarred = request.IsStarred,
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _unitOfWork.CategoryRepository.InsertOne(category);

            return category;
        }

        public Category Update(Category request)
        {
            if (string.IsNullOrEmpty(request.Name))
                BadRequest(Messages.NameRequired);

            var getRequest = new GetCategoryByIdRequest
            {
                UserId = request.UserId,
                Id = request.Id
            };

            Category category = GetById(getRequest);

            if (category == null)
                NotFound();

            category.Name = request.Name;
            category.Color = request.Color;
            category.Label = request.Label;
            category.IsStarred = request.IsStarred;
            category.UpdatedDate = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.ReplaceOne(category);

            return category;
        }

        public Category Delete(DeleteCategoryRequest request)
        {
            _unitOfWork.ItemRepository.DeleteMany(i => i.CategoryId == request.Id);

            var category = _unitOfWork.CategoryRepository.DeleteOne(c => (c.UserId == request.UserId) && (c.Id == request.Id));

            return category;
        }
    }
}
