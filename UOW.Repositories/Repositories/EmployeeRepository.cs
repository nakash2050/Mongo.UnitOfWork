using UOW.Entities.Domain;
using UOW.Repositories.Concretes;
using UOW.Repositories.Contracts;

namespace UOW.Repositories.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IMongoContext mongoContext) : base(mongoContext)
        {

        }
    }
}
