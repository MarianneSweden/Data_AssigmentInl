using Business.Services;
using Business.Models;

namespace Presentation_ConsoleApp.Dialogs
{
    public class ProductDialog
    {
        private readonly ProductService _productService;

        public ProductDialog(ProductService productService)
        {
            _productService = productService;
        }

        public async Task ShowProductMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("* * * Product Management * * * ");
                Console.WriteLine("1. Create New Product");
                Console.WriteLine("2. View All Products");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Choose an option: ");


                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        await CreateProductAsync();
                        break;
                    case "2":
                        await ShowAllProductsAsync();
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

        public async Task CreateProductAsync()
        {
            try
            {
                Console.Write("Enter the name of the product: ");
                string productName = Console.ReadLine() ?? string.Empty;

                Console.Write("Enter the price of the product:");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Incorrect format for price.");
                    return;
                }

                var product = new ProductModel { ProductName = productName, Price = price };
                await _productService.AddProductAsync(product);
                Console.WriteLine($"Product '{productName}' has been created !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task ShowAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.WriteLine("List of products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.ProductName}, Price: {product.Price}");
            }
        }
    }
}
