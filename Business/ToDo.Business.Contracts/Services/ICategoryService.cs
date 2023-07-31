using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Client.Entities.Responses;
using ToDo.Client.Entities.Responses.Category;

namespace ToDo.Business.Contracts.Services
{
    public interface ICategoryService
    {
        ApiResponse Get(GetCategoryRequest request);
        ApiResponse GetById(GetCategoryByIdRequest request);
        ApiResponse Add(AddCategoryRequest request);
        ApiResponse Update(UpdateCategoryRequest request);
        ApiResponse Delete(DeleteCategoryRequest request);
    }
}
