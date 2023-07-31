using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using ToDo.Common.Configurations;
using ToDo.Business.Entities.Models;
using CloudGateway.Business.Contracts;

namespace ToDo.Data
{
    public class MongoRepository<T> : IMongoRepository<T>
        where T : BaseEntity
    {
        private IMongoCollection<T> _collection;

        public MongoRepository()
        {
            var database = new MongoClient(MongoDbConfiguration.ConnectionString).GetDatabase(MongoDbConfiguration.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(_ => true).ToEnumerable();
        }
        
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter).ToEnumerable();
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter).SingleOrDefault();
        }

        public T GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);

            return _collection.Find(filter).SingleOrDefault();
        }

        public void InsertOne(T record)
        {
            if (string.IsNullOrEmpty(record.Id))
                record.Id = Guid.NewGuid().ToString();

            _collection.InsertOne(record);
        }

        public void ReplaceOne(T record)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, record.Id);

            _collection.ReplaceOne(filter, record);
        }

        public T DeleteOne(Expression<Func<T, bool>> filter)
        {
            return _collection.FindOneAndDelete(filter);
        }

        public void DeleteMany(Expression<Func<T, bool>> filter)
        {
            _collection.DeleteMany(filter);
        }

        public T DeleteById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
        
            return _collection.FindOneAndDelete(filter);
        }
    }
}
