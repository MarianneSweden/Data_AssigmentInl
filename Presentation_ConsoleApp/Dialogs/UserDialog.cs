using Business.Services;
using Business.Models;


namespace Presentation_ConsoleApp.Dialogs
{
    public class UserDialog
    {
        private readonly UserService _userService;

        public UserDialog(UserService userService)
        {
            _userService = userService;
        }

        public async Task ShowUserMenuAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- User Management ---");
                Console.WriteLine("1. Create New User");
                Console.WriteLine("2. View All Users");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Choose an option: ");


                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        await CreateUserAsync();
                        break;
                    case "2":
                        Console.Clear();
                        await ShowAllUsersAsync();
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

        public async Task CreateUserAsync()
        {
            try
            {
                Console.Write("Firstname: ");
                string firstName = Console.ReadLine() ?? string.Empty;

                Console.Write("Lastname: ");
                string lastName = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine("First name and last name cannot be empty!");
                    return;
                }

                var user = new UserModel
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                await _userService.AddUserAsync(user);
                Console.WriteLine($"The user {firstName} {lastName} has been created");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }

        public async Task ShowAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                if (!users.Any())
                {
                    Console.WriteLine("No users found.");
                    return;
                }

                Console.WriteLine("List of users:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Name: {user.FirstName} {user.LastName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving users: {ex.Message}");
            }
        }
    }
}
