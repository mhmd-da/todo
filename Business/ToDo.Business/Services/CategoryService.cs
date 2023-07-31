using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using ToDo.Business.Contracts.Engines;
using ToDo.Business.Contracts.Services;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Client.Entities.Responses;
using ToDo.Client.Entities.Responses.Category;
using ToDo.Common.Static;

namespace ToDo.Business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryEngine _categoryEngine;

        public CategoryService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _categoryEngine = serviceProvider.GetRequiredService<ICategoryEngine>();
        }

        public ApiResponse Get(GetCategoryRequest request)
        {
            List<Category> categories = _categoryEngine.Get(request);

            if (categories == null || categories.Count == 0)
                NotFound(Messages.CategoriesNotFound);

            List<GetCategoryResponse> response = _autoMapperLoader.Mapper.Map<List<GetCategoryResponse>>(categories);

            return Success(response);
        }

        public ApiResponse GetById(GetCategoryByIdRequest request)
        {
            Category category = _categoryEngine.GetById(request);

            if (category == null)
                NotFound(string.Format(Messages.CategoryNotFound, request.Id));

            GetCategoryResponse response = _autoMapperLoader.Mapper.Map<GetCategoryResponse>(category);

            return Success(response);
        }

        public ApiResponse Add(AddCategoryRequest request)
        {
            Category category = _autoMapperLoader.Mapper.Map<Category>(request);

            category = _categoryEngine.Add(category);

            if (category == null)
                BadRequest(Messages.CreateCategoryError);

            GetCategoryResponse response = _autoMapperLoader.Mapper.Map<GetCategoryResponse>(category);

            return Created(response);
        }

        public ApiResponse Update(UpdateCategoryRequest request)
        {
            Category category = _autoMapperLoader.Mapper.Map<Category>(request);

            category = _categoryEngine.Update(category);

            if (category == null)
                BadRequest(Messages.UpdateCategoryError);

            GetCategoryResponse response = _autoMapperLoader.Mapper.Map<GetCategoryResponse>(category);

            return Success(response);
        }

        public ApiResponse Delete(DeleteCategoryRequest request)
        {
            Category category = _categoryEngine.Delete(request);

            if (category == null)
                BadRequest(Messages.DeleteCategoryError);

            GetCategoryResponse response = _autoMapperLoader.Mapper.Map<GetCategoryResponse>(category);

            return Success(response);
        }
    }
}
