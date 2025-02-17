using Business.Services;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Factories
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            // Register Repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Register Services
            services.AddScoped<CustomerService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<StatusTypeService>();
            services.AddScoped<UserService>();
        }
    }
}
