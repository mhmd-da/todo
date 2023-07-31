using CloudGateway.Business.Contracts;
using ToDo.Business.Entities.Models;

namespace ToDo.Data.Contracts.Repositories
{
    public interface ICategoryRepository : IMongoRepository<Category>
    {
    }
}
