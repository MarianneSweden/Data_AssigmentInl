using Business.Services;
using Business.Models;

namespace Presentation_ConsoleApp.Dialogs;

public class ProjectDialog
{
    private readonly ProjectService _projectService;

    public ProjectDialog(ProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task ShowProjectMenuAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Project Management ---");
            Console.WriteLine("1. Create a new project");
            Console.WriteLine("2. View all projects");
            Console.WriteLine("3. View and update project");
            Console.WriteLine("4. Delete a project");
            Console.WriteLine("5. Back to main menu");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    await CreateProjectAsync();
                    break;
                case "2":
                    await ShowAllProjectsAsync();
                    break;
                case "3":
                    await UpdateProjectAsync();
                    break;
                  case "4":
                    await DeleteProjectAsync();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }


    //Create är klar
    public async Task CreateProjectAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("Create a new project");

            Console.Write("Enter project name:");
            string projectName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter project description: ");
            string description = Console.ReadLine() ?? string.Empty;

            DateTime startDate;
            while (true)
            {
                Console.Write("Enter start date (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out startDate))
                    break;
                Console.WriteLine("Invalid format. Enter date in YYYY-MM-DD format.");
            }

            Console.Write("Enter end date (YYYY-MM-DD) or press enter if open-ended: ");
            string? endDateInput = Console.ReadLine();
            DateTime? endDate = string.IsNullOrWhiteSpace(endDateInput) ? null : DateTime.Parse(endDateInput);



            int customerId = GetValidInt("Enter customer ID: ");
          

            int statusTypeId = GetValidInt("Enter status (1: Not Started, 2: In Progress, 3: Completed): ");

            int userId = GetValidInt("Enter user ID: ");

            int productId = GetValidInt("Enter product ID: ");
           

           
            decimal estimatedHours = GetValidDecimal("Enter estimated hours: ");
           


            decimal pricePerHour = GetValidDecimal("Enter price per hour: ");

      



            var project = new ProjectModel
            {
                ProjectName = projectName,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = customerId,
                StatusId = statusTypeId,
                UserId = userId,
                ProductId = productId,
                EstimatedHours = estimatedHours,
                PricePerHour = pricePerHour
            };


       


            await _projectService.AddProjectAsync(project);
            Console.WriteLine($"Project '{projectName}' has been created!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred:  {ex.Message}");
        }
    }




    //Show all klar

    public async Task ShowAllProjectsAsync()
    {
        var projects = await _projectService.GetAllProjectsAsync();

        if (!projects.Any())
        {
            Console.WriteLine("No projects found.");
            return;
        }

        Console.WriteLine("List of projects:");
        foreach (var project in projects)
        {
            string endDateText = project.EndDate?.ToString("yyyy-MM-dd") ?? "Ongoing";
            Console.WriteLine($" {project.ProjectNumber} - {project.ProjectName}, " +$"{project.StartDate:yyyy-MM-dd} to {endDateText}, " +
                $"Status: {GetStatusName(project.StatusId)}, " +
                $"Price: {project.PricePerHour:C} /h, " + $"Estimated hours: {project.EstimatedHours}");
        }
    }



    // klar 
    public async Task UpdateProjectAsync()
    {
        Console.Clear();
        Console.WriteLine("View & Update Project Details");

        var projects = await _projectService.GetAllProjectsAsync();
        if (!projects.Any())
        {
            Console.WriteLine("No projects found.");
            return;
        }

        Console.WriteLine("List of projects:");
        foreach (var project in projects)
        {
            Console.WriteLine($" {project.ProjectNumber} - {project.ProjectName} , Description: {project.Description}");
        }

        Console.Write("Enter the Project Number to view details: ");
        string projectNumber = Console.ReadLine() ?? "";

        var projectToUpdate = projects.FirstOrDefault(p => p.ProjectNumber == projectNumber);

        if (projectToUpdate == null)
        {
            Console.WriteLine("Project not found.");
            return;
        }



        Console.WriteLine("* * * Project Details * * *");
        Console.WriteLine($"Project Number: {projectToUpdate.ProjectNumber}");
        Console.WriteLine($"Project Name: {projectToUpdate.ProjectName}");
        Console.WriteLine($"Description: {projectToUpdate.Description}");
        Console.WriteLine($"Start Date: {projectToUpdate.StartDate:yyyy-MM-dd}");
        Console.WriteLine($"End Date: {projectToUpdate.EndDate?.ToString("yyyy-MM-dd") ?? "Ongoing"}");
        Console.WriteLine($"Customer ID: {projectToUpdate.CustomerId}");
        Console.WriteLine($"User ID: {projectToUpdate.UserId}");
        Console.WriteLine($"Product ID: {projectToUpdate.ProductId}");
        Console.WriteLine($"Status: {GetStatusName(projectToUpdate.StatusId)}");
        Console.WriteLine($"Estimated Hours: {projectToUpdate.EstimatedHours}");
        Console.WriteLine($"Price per Hour: {projectToUpdate.PricePerHour}");
        Console.WriteLine($"Total Price: {projectToUpdate.TotalPrice}");



        Console.Write("Do you want to update this project? (yes/no): ");
        string updateChoice = Console.ReadLine()?.ToLower() ?? "";


        if (updateChoice == "yes")
        {
            Console.Write("Enter new project name (leave empty to keep current): ");
            string? newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) projectToUpdate.ProjectName = newName;


            Console.Write("Enter new description (leave empty to keep current): ");
            string? newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription)) projectToUpdate.Description = newDescription;


            projectToUpdate.StatusId = GetValidInt("New status (1: Not Started, 2: In Progress, 3: Completed): ");


            projectToUpdate.EstimatedHours = GetValidDecimal("New estimated hours (leave empty to keep current): ");


           
            projectToUpdate.PricePerHour = GetValidDecimal("New price per hour (leave empty to keep current): ");
          


            if (projectToUpdate.StatusId == 3)
            {
                Console.Write("Enter end date (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    projectToUpdate.EndDate = endDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }



            await _projectService.UpdateProjectAsync(projectToUpdate);
            Console.WriteLine("Project has been updated successfully!");
        }
    }


    public async Task DeleteProjectAsync()
    {
        Console.Clear();
        Console.WriteLine("Delete a project");

        var projects = await _projectService.GetAllProjectsAsync();
        if (!projects.Any())
        {
            Console.WriteLine("No projects found.");
            return;
        }

        Console.WriteLine("List of projects:");
        foreach (var project in projects)
        {
            Console.WriteLine($"{project.Id} - {project.ProjectNumber} - {project.ProjectName}");
        }

        int projectId = GetValidInt("Enter the ID of the project you want to delete: ");

        Console.Write("Are you sure you want to delete this project? (yes/no): ");
        string confirmation = Console.ReadLine()?.Trim().ToLower() ?? "";

        if (confirmation == "yes")
        {
            bool success = await _projectService.DeleteProjectAsync(projectId);
            if (success)
                Console.WriteLine("Project deleted successfully!");
            else
                Console.WriteLine("Failed to delete project.");
        }
        else
        {
            Console.WriteLine("Project deletion canceled.");
        }
    }


    private int GetValidInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Please enter a valid integer.");
        }
    }

    private decimal GetValidDecimal(string prompt)
    {
        decimal value;
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out value))
                return value;
            Console.WriteLine("Please enter a valid decimal number.");
        }
    }

    private string GetStatusName(int statusId)
    {
        return statusId switch
        {
            1 => "Not Started",
            2 => "In Progress",
            3 => "Completed",
            _ => "Unknown Status"
        };
    }
}
