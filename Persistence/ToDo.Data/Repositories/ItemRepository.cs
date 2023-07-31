using CloudGateway.Business.Contracts;
using System;
using ToDo.Business.Entities.Models;
using ToDo.Common.Enums;
using ToDo.Data.Contracts.Repositories;

namespace ToDo.Data.Repositories
{
    public class ItemRepository : MongoRepository<Item>, IItemRepository
    {
    }
}
