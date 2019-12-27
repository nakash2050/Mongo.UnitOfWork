using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using UOW.Repositories.Contracts;
using ServiceStack;
using MongoDB.Bson;

namespace UOW.Repositories.Concretes
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoContext mongoContext;
        protected IMongoCollection<T> DBSet;

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

            var mid = ObjectId.Parse(id);

            var data = await DBSet.FindAsync(Builders<T>.Filter.Eq("_id", mid));
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            ConfigDbSet();
            var data = await DBSet.FindAsync(Builders<T>.Filter.Empty);
            return await data.ToListAsync();
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

        public void Dispose()
        {
            mongoContext?.Dispose();
        }
    }
}
