using Business.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Presentation_ConsoleApp.Dialogs
{
    public class MainMenuDialog
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProjectService _projectService;
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public MainMenuDialog(IServiceProvider serviceProvider,
                               ProjectService projectService,
                               CustomerService customerService,
                               ProductService productService,
                               UserService userService)
        {
            _serviceProvider = serviceProvider;
          _projectService = projectService;
            _customerService = customerService;
            _productService = productService;
            _userService = userService;
        }

        public async Task ShowMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("* * * * * Pennywise AB * * * * * ");
                Console.WriteLine("1 Manage Projects");
                Console.WriteLine("2 Manage Customers");
                Console.WriteLine("3 Manage Products");
                Console.WriteLine("4 Manage Users");
                Console.WriteLine("5 Exit");
                Console.Write("Choose an option: ");


                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        var projectDialog = _serviceProvider.GetRequiredService<ProjectDialog>();
                        await projectDialog.ShowProjectMenuAsync();
                        break;

                    case "2":
                        Console.Clear();
                        var customerDialog = _serviceProvider.GetRequiredService<CustomerDialog>();
                        await customerDialog.ShowCustomerMenuAsync();
                        break;

                    case "3":
                        Console.Clear();
                        var productDialog = _serviceProvider.GetRequiredService<ProductDialog>();
                        await productDialog.ShowProductMenuAsync();
                        break;

                    case "4":
                        Console.Clear();
                        var userDialog = _serviceProvider.GetRequiredService<UserDialog>();
                        await userDialog.ShowUserMenuAsync();
                        break;

                    case "5":
                        Console.WriteLine("Exiting the program... Thank you for using Pennywise AB");
                        return;

                    default:
                        Console.WriteLine("Invalid selection.Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
