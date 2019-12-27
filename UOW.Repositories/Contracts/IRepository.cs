using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UOW.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(string id);

        void Add(T entity);

        void AddMany(IEnumerable<T> entity);

        void Update(T entity);

        void Remove(string id);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
