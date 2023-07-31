using Microsoft.Extensions.DependencyInjection;
using ToDo.Business.Contracts.Services;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Client.Entities.Responses;
using ToDo.Data.Contracts.Repositories;
using ToDo.Business.Exceptions;
using ToDo.Client.Entities.Responses.Category;

namespace ToDo.UnitTest.Controllers
{
    public class CategoryTest : BaseTest
    {
        private readonly ICategoryService _categoryService;

        public CategoryTest()
        {
            _categoryService = _serviceProvider.GetRequiredService<ICategoryService>();
        }


        [Fact]
        public async Task GetCategories_return_Ok()
        {
            // Arrange
            var category = CreateCategoryData();
            var categoryResponse = _autoMapper.Mapper.Map<GetCategoryResponse>(category);
            var expectedResult = new List<GetCategoryResponse>() { categoryResponse };

            // Act
            var request = new GetCategoryRequest
            {
                UserId = category.UserId
            };
            var result = _categoryService.Get(request);
            var actualResult = (List<GetCategoryResponse>)result.Data;

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
            Assert.Equal(expectedResult.Count, actualResult.Count);
            foreach (var exp in expectedResult)
            {
                Assert.True(actualResult.Exists(a => a.Id == exp.Id));
            }
        }

        [Fact]
        public async Task GetCategories_return_NotFound()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();

            // Act
            ApiResponse result = null;
            var request = new GetCategoryRequest
            {
                UserId = userId
            };
            try
            {
                result = _categoryService.Get(request);
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
            var expectedResult = _autoMapper.Mapper.Map<GetCategoryResponse>(category);

            // Act
            var request = new GetCategoryByIdRequest
            {
                Id = category.Id,
                UserId = category.UserId
            };
            var result = _categoryService.GetById(request);
            var actualResult = (GetCategoryResponse)result.Data;

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
            Assert.Equal(expectedResult.Id, actualResult.Id);
        }

        [Fact]
        public async Task GetCategoryById_return_NotFound()
        {
            // Arrange
            string userId = Guid.NewGuid().ToString();

            // Act
            ApiResponse result = null;
            var request = new GetCategoryByIdRequest
            {
                UserId = userId
            };
            try
            {
                result = _categoryService.GetById(request);
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
        public async Task AddCategory_return_Created()
        {
            // Arrange

            // Act
            var request = new AddCategoryRequest
            {
                UserId = Guid.NewGuid().ToString(),
                Name = "name",
                Label = Common.Enums.Label.Default,
                Color = "label",
                IsStarred = false
            };
            var result = _categoryService.Add(request);

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task AddCategory_EmptyName_return_BadRequest()
        {
            // Arrange

            // Act
            var request = new AddCategoryRequest
            {
                UserId = Guid.NewGuid().ToString(),
                Label = Common.Enums.Label.Default,
                Color = "label",
                IsStarred = false
            };

            ApiResponse result;
            try
            {
                result = _categoryService.Add(request);
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
        public async Task UpdateCategory_return_Ok()
        {
            // Arrange
            var category = CreateCategoryData();

            // Act
            var request = new UpdateCategoryRequest
            {
                Id = category.Id,
                UserId = category.UserId,
                Name = "UPDATED",
                Label = category.Label,
                Color = category.Color,
                IsStarred = true
            };
            var result = _categoryService.Update(request);
            var actualResult = (GetCategoryResponse)result.Data;

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
            Assert.Equal(request.Name, actualResult.Name);
        }

        [Fact]
        public async Task UpdateCategory_EmptyName_return_BadRequest()
        {
            // Arrange
            var category = CreateCategoryData();

            // Act
            var request = new UpdateCategoryRequest
            {
                Id = category.Id,
                UserId = category.UserId,
                Label = category.Label,
                Color = category.Color,
                IsStarred = true
            };

            ApiResponse result;
            try
            {
                result = _categoryService.Update(request);
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
        public async Task UpdateCategory_WrongId_return_NotFound()
        {
            // Arrange
            var category = CreateCategoryData();

            // Act
            var request = new UpdateCategoryRequest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = category.UserId,
                Name = "UPDATED",
                Label = category.Label,
                Color = category.Color,
                IsStarred = true
            };

            ApiResponse result;
            try
            {
                result = _categoryService.Update(request);
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

            // Act
            var request = new DeleteCategoryRequest
            {
                Id = category.Id,
                UserId = category.UserId
            };
            var result = _categoryService.Delete(request);

            // Assert
            AssertWithSuccess(result, System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteCategory_WrongId_return_BadRequest()
        {
            // Arrange

            // Act
            var request = new DeleteCategoryRequest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString()
            };

            ApiResponse result;
            try
            {
                result = _categoryService.Delete(request);
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


        #region AssertWithSuccess


        #endregion
    }
}
