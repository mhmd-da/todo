using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http;
using ToDo.API.Controllers;
using ToDo.Business.Contracts.Services;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Client.Entities.Responses;
using ToDo.Data.Contracts.Repositories;
using ToDo.Data.Repositories;
using ToDo.Business.Entities.Models;
using ToDo.Business.Exceptions;
using ToDo.Client.Entities.Responses.Category;
using System.Collections.Immutable;
using ToDo.Common.Static;
using Microsoft.EntityFrameworkCore;
using ToDo.Business.Contracts.Engines;
using ToDo.Client.Entities.Responses.Item;
using ToDo.Client.Entities.Requests.Item;

namespace ToDo.UnitTest.Controllers
{
    public class ItemTest : BaseTest
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemService _itemService;

        public ItemTest()
        {
            _itemRepository = _serviceProvider.GetRequiredService<IItemRepository>();
            _itemService = _serviceProvider.GetRequiredService<IItemService>();
        }


        [Fact]
        public async Task GetItems_return_Ok()
        {
            // Arrange
            var category = CreateCategoryData();
            var item = CreateItemData(category.Id);
            var itemResponse = _autoMapper.Mapper.Map<GetItemResponse>(item);
            var expectedResult = new List<GetItemResponse>() { itemResponse };

            // Act
            var request = new GetItemRequest
            {
                UserId = category.UserId,
                CategoryId = category.Id
            };
            var result = _itemService.Get(request);
            var actualResult = (List<GetItemResponse>)result.Data;

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
            Assert.Equal(expectedResult.Count, actualResult.Count);
            foreach (var exp in expectedResult)
            {
                Assert.True(actualResult.Exists(a => a.Id == exp.Id));
            }
        }

        [Fact]
        public async Task GetItems_CategoryFoundButNoItems_return_NotFound()
        {
            // Arrange
            var category = CreateCategoryData();

            // Act
            ApiResponse result = null;
            var request = new GetItemRequest
            {
                UserId = category.UserId,
                CategoryId = category.Id
            };
            try
            {
                result = _itemService.Get(request);
            }
            catch (NotFoundException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.Status);
        }

        [Fact]
        public async Task GetItems_CategoryNotFound_return_NotFound()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString();

            // Act
            ApiResponse result = null;
            var request = new GetItemRequest
            {
                UserId = guid,
                CategoryId = guid
            };
            try
            {
                result = _itemService.Get(request);
            }
            catch (NotFoundException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.Status);
        }

        [Fact]
        public async Task GetCategoryById_return_Ok()
        {
            // Arrange
            var category = CreateCategoryData();
            var item = CreateItemData(category.Id);
            var expectedResult = _autoMapper.Mapper.Map<GetItemResponse>(item);

            // Act
            var request = new GetItemByIdRequest
            {
                Id = item.Id,
                CategoryId = category.Id,
                UserId = category.UserId
            };
            var result = _itemService.GetById(request);
            var actualResult = (GetItemResponse)result.Data;

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
            Assert.Equal(expectedResult.Id, actualResult.Id);
        }

        [Fact]
        public async Task GetItemById_CategoryFoundButItemNotFound_return_NotFound()
        {
            // Arrange
            var category = CreateCategoryData();
            string guid = Guid.NewGuid().ToString();

            // Act
            ApiResponse result = null;
            var request = new GetItemByIdRequest
            {
                UserId = category.UserId,
                Id = guid,
                CategoryId = category.Id
            };
            try
            {
                result = _itemService.GetById(request);
            }
            catch (NotFoundException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.Status);
        }

        [Fact]
        public async Task GetItemById_CategoryNotFound_return_NotFound()
        {
            // Arrange
            string guid = Guid.NewGuid().ToString();

            // Act
            ApiResponse result = null;
            var request = new GetItemByIdRequest
            {
                UserId = guid,
                Id = guid,
                CategoryId = guid
            };
            try
            {
                result = _itemService.GetById(request);
            }
            catch (NotFoundException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.Status);
        }

        [Fact]
        public async Task AddItem_return_Created()
        {
            // Arrange
            var category = CreateCategoryData();

            // Act
            var request = new AddItemRequest
            {
                Name = "Test",
                Content = "An item content containing the ToDo action",
                DueDate = DateTime.UtcNow,
                UserId = category.UserId,
                CategoryId = category.Id
            };
            var result = _itemService.Add(request);

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddItem_CategoryNotFound_return_NotFound()
        {
            // Arrange
            string guid = Guid.NewGuid().ToString();

            // Act
            var request = new AddItemRequest
            {
                Name = "Test",
                Content = "An item content containing the ToDo action",
                DueDate = DateTime.UtcNow,
                UserId = guid,
                CategoryId = guid
            };

            ApiResponse result;
            try
            {
                result = _itemService.Add(request);
            }
            catch (NotFoundException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.Status);
        }

        [Fact]
        public async Task UpdateItem_return_Ok()
        {
            // Arrange
            var category = CreateCategoryData();
            var item = CreateItemData(category.Id);

            // Act
            var request = new UpdateItemRequest
            {
                Id = item.Id,
                UserId = category.UserId,
                CategoryId = category.Id,
                Content = "UPDATED",
                IsDone = true,
                DueDate = DateTime.UtcNow
            };
            var result = _itemService.Update(request);
            var actualResult = (GetItemResponse)result.Data;

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
            Assert.Equal(request.Name, actualResult.Name);
        }

        [Fact]
        public async Task UpdateItem_CategoryFoundButItemNotFound_return_BadRequest()
        {
            // Arrange
            var category = CreateCategoryData();
            string guid = Guid.NewGuid().ToString();

            // Act
            var request = new UpdateItemRequest
            {
                Id = guid,
                UserId = category.UserId,
                CategoryId = category.Id,
                Content = "UPDATED",
                IsDone = true,
                DueDate = DateTime.UtcNow
            };

            ApiResponse result;
            try
            {
                result = _itemService.Update(request);
            }
            catch (BadRequestException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.Status);
        }

        [Fact]
        public async Task UpdateItem_CategoryNotFound_return_NotFound()
        {
            // Arrange
            string guid = Guid.NewGuid().ToString();

            // Act
            var request = new UpdateItemRequest
            {
                Id = guid,
                UserId = guid,
                CategoryId = guid,
                Content = "UPDATED",
                IsDone = true,
                DueDate = DateTime.UtcNow
            };

            ApiResponse result;
            try
            {
                result = _itemService.Update(request);
            }
            catch (NotFoundException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.Status);
        }

        [Fact]
        public async Task DeleteCategory_return_Ok()
        {
            // Arrange
            var category = CreateCategoryData();
            var item = CreateItemData(category.Id);

            // Act
            var request = new DeleteItemRequest
            {
                Id = item.Id,
                UserId = category.UserId,
                CategoryId = category.Id
            };
            var result = _itemService.Delete(request);

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteItem_CategoryFoundButItemNotFound_return_BadRequest()
        {
            // Arrange
            var category = CreateCategoryData();
            string guid = Guid.NewGuid().ToString();

            // Act
            var request = new DeleteItemRequest
            {
                Id = guid,
                UserId = category.UserId,
                CategoryId = category.Id
            };

            ApiResponse result;
            try
            {
                result = _itemService.Delete(request);
            }
            catch (BadRequestException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.Status);
        }

        [Fact]
        public async Task DeleteItem_CategoryNotFound_return_NotFound()
        {
            // Arrange
            string guid = Guid.NewGuid().ToString();

            // Act
            var request = new DeleteItemRequest
            {
                Id = guid,
                UserId = guid,
                CategoryId = guid
            };

            ApiResponse result;
            try
            {
                result = _itemService.Delete(request);
            }
            catch (NotFoundException ex)
            {
                result = ex.Response;
            }

            // Assert
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.Status);
        }


        #region AssertWithSuccess


        private Item CreateItemData(string categoryId)
        {
            Item item = new Item
            {
                Name = "Test",
                Content = "An item content containing the ToDo action",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDone = false,
                DueDate = DateTime.UtcNow,
                CategoryId = categoryId
            };
            _itemRepository.InsertOne(item);

            return item;
        }

        #endregion
    }
}
