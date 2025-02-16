using Business.Models;
using Business.Factories;
using Data.Interfaces;

namespace Business.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task AddCustomerAsync(CustomerModel model)
        {
            var entity = CustomerFactory.Create(model);
            await _customerRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(CustomerFactory.Create);
        }

        public async Task<CustomerModel?> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer != null ? CustomerFactory.Create(customer) : null;
        }

        public async Task UpdateCustomerAsync(CustomerModel model)
        {
            var entity = CustomerFactory.Create(model);
            await _customerRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteAsync(id);
        }
    }
}
