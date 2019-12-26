using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using UOW.Repositories.Contracts;
using ServiceStack;

namespace UOW.Repositories.Concretes
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoContext mongoContext;
        private IMongoCollection<T> DBSet;

        public Repository(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        private void ConfigDbSet()
        {
            DBSet = mongoContext.GetCollection<T>(typeof(T).Name);
        }

        public virtual async Task<T> Get(string id)
        {
            ConfigDbSet();
            var data = await DBSet.FindAsync(Builders<T>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            ConfigDbSet();
            var data = await DBSet.FindAsync(Builders<T>.Filter.Empty);
            return data.ToList();
        }

        public virtual void Remove(string id)
        {
            ConfigDbSet();
            mongoContext.AddCommand(() => DBSet.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id)));
        }

        public virtual void Update(T entity)
        {
            ConfigDbSet();
            mongoContext.AddCommand(() => DBSet.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", entity.GetId()), entity));
        }

        public virtual void Add(T entity)
        {
            ConfigDbSet();
            mongoContext.AddCommand(() => DBSet.InsertOneAsync(entity));
        }
    }
}
