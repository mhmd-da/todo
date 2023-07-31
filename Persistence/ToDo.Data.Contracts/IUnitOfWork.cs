using ToDo.Data.Contracts.Repositories;

namespace ToDo.Data.Contracts
{
    public interface IUnitOfWork
    {
        IItemRepository ItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }
    }
}
