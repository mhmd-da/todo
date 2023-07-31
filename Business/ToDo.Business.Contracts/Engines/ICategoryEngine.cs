using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Requests.Category;

namespace ToDo.Business.Contracts.Engines
{
    public interface ICategoryEngine
    {
        List<Category> Get(GetCategoryRequest request);
        Category GetById(GetCategoryByIdRequest request);
        Category Add(Category request);
        Category Update(Category request);
        Category Delete(DeleteCategoryRequest request);
    }
}
