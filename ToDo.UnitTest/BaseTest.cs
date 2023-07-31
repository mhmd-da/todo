using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.AutoMapper;
using ToDo.Business.Bootstrapper;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Responses;
using ToDo.Data.Contracts.Repositories;

namespace ToDo.UnitTest
{
    public class BaseTest
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly AutoMapperLoader _autoMapper;
        protected readonly ICategoryRepository _categoryRepository;

        public BaseTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.Init();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _autoMapper = _serviceProvider.GetRequiredService<AutoMapperLoader>();
            _categoryRepository = _serviceProvider.GetRequiredService<ICategoryRepository>();
        }

        protected void AssertWithSuccess(ApiResponse result, HttpStatusCode status)
        {
            Assert.NotNull(result.Data);
            Assert.Equal("Success", result.Message);
            Assert.True(result.Success);
            Assert.Equal(status, result.Status);
        }

        protected Category CreateCategoryData()
        {
            Category category = new Category
            {
                Name = "Test",
                Color = "Black",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Label = Common.Enums.Label.Default,
                UserId = Guid.NewGuid().ToString(),
                IsStarred = false
            };
            _categoryRepository.InsertOne(category);

            return category;
        }
    }
}
