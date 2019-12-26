using System.Threading.Tasks;
using UOW.Repositories.Contracts;

namespace UOW.Repositories.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext mongoContext;

        public UnitOfWork(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await mongoContext.SaveChanges();
            return changeAmount > 0;
        }

        public void Dispose()
        {
            mongoContext.Dispose();
        }
    }
}
