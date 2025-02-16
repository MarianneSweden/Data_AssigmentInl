using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation_ConsoleApp.Dialogs;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(options =>
        options
            .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\ProjectSchool\\Data_Assigment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True")
            .UseLazyLoadingProxies())

    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IUserRepository, UserRepository>() // NYTT

    .AddScoped<CustomerService>()
    .AddScoped<ProjectService>()
    .AddScoped<ProductService>()
    .AddScoped<UserService>() // NYTT

    .AddScoped<MainMenuDialog>()
    .AddScoped<CustomerDialog>()
    .AddScoped<ProjectDialog>()
    .AddScoped<ProductDialog>()
    .AddScoped<UserDialog>(); // NYTT

using var serviceProvider = services.BuildServiceProvider();

var mainMenuDialog = serviceProvider.GetRequiredService<MainMenuDialog>();
await mainMenuDialog.ShowMenuAsync();
