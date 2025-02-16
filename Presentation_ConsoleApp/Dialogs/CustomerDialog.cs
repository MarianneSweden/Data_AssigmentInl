using Business.Services;
using Business.Models;

namespace Presentation_ConsoleApp.Dialogs
{
    public class CustomerDialog
    {
        private readonly CustomerService _customerService;

        public CustomerDialog(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task ShowCustomerMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("**** customer management ****");
                Console.WriteLine("1. Create new customer");
                Console.WriteLine("2. Show all customers");
                Console.WriteLine("3. Back to main menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        await CreateCustomerAsync();
                        break;
                    case "2":
                        Console.Clear();
                        await ShowAllCustomersAsync();
                        break;
                    case "3":
                        return; 
                    default:
                        Console.WriteLine("Wrong choice. Try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public async Task CreateCustomerAsync()
        {
            try
            {
                Console.WriteLine("Enter the customer's name:");
                string customerName = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(customerName))
                {
                    Console.WriteLine("Name cannot be empty");
                    return;
                }

                var customer = new CustomerModel { CustomerName = customerName };
                await _customerService.AddCustomerAsync(customer);
                Console.WriteLine($"Custpomer '{customerName}' has been created");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }

        public async Task ShowAllCustomersAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();

                if (!customers.Any())
                {
                    Console.WriteLine("No customers found");
                    return;
                }

                Console.WriteLine("List of customers:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($" ID: {customer.Id}, Name: {customer.CustomerName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customers: {ex.Message}");
            }
        }
    }
}
