using Microsoft.Extensions.DependencyInjection;
using System;
using ToDo.Data.Contracts;
using ToDo.Data.Contracts.Repositories;

namespace ToDo.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Repositories
        
        public IItemRepository ItemRepository { get; }
        public ICategoryRepository CategoryRepository { get; }

        #endregion

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            ItemRepository = serviceProvider.GetRequiredService<IItemRepository>();
            CategoryRepository = serviceProvider.GetRequiredService<ICategoryRepository>();
        }

    }
}
