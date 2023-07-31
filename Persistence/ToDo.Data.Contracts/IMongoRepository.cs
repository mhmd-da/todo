using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ToDo.Business.Entities.Models;

namespace CloudGateway.Business.Contracts
{
    public interface IMongoRepository<T>
        where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> filter);
        T GetOne(Expression<Func<T, bool>> filter);
        T GetById(string id);

        void InsertOne(T record);

        void ReplaceOne(T record);

        T DeleteOne(Expression<Func<T, bool>> filter);
        void DeleteMany(Expression<Func<T, bool>> filter);
        T DeleteById(string id);
    }
}
