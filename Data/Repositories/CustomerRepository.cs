using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class CustomerRepository : BaseRepository<CustomerEntity>, ICustomerRepository

    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }
    }
}
