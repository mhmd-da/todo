using ToDo.Client.Entities.Requests.Item;
using ToDo.Client.Entities.Responses;

namespace ToDo.Business.Contracts.Engines
{
    public interface IItemService
    {
        ApiResponse Get(GetItemRequest request);
        ApiResponse GetById(GetItemByIdRequest request);
        ApiResponse Add(AddItemRequest request);
        ApiResponse Update(UpdateItemRequest request);
        ApiResponse Delete(DeleteItemRequest request);
    }
}
