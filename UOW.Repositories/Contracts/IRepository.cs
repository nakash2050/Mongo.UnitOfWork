using System.Collections.Generic;
using System.Threading.Tasks;

namespace UOW.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(string id);

        void Add(T entity);

        void Update(T entity);

        void Remove(string id);
    }
}
