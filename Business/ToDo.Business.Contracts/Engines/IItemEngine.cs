using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Requests.Item;

namespace ToDo.Business.Contracts.Engines
{
    public interface IItemEngine
    {
        List<Item> Get(GetItemRequest request);
        Item GetById(GetItemByIdRequest request);
        Item Add(Item request, string userId);
        Item Update(Item request, string userId);
        Item Delete(DeleteItemRequest request);

    }
}
