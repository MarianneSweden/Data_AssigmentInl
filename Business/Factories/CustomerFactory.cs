using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class CustomerFactory
    {
        public static CustomerEntity Create(CustomerModel model)
        {
            return new CustomerEntity
            {
                Id = model.Id,
                CustomerName = model.CustomerName
            };
        }

        public static CustomerModel Create(CustomerEntity entity)
        {
            return new CustomerModel
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName
            };
        }
    }
}
