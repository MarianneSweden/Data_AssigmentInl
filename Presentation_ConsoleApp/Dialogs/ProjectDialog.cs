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
            Console.WriteLine("3. Update project");
            Console.WriteLine("4. Back to main menu");
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
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

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
            Console.WriteLine($" {project.ProjectNumber} - {project.ProjectName}, {project.StartDate:yyyy-MM-dd} to {endDateText}, Status: {GetStatusName(project.StatusId)}, Price: {project.PricePerHour:C} /h, Estimated hours: {project.EstimatedHours}");
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

    public async Task UpdateProjectAsync()
    {
        Console.Clear();
        Console.WriteLine("Update a project");

        var projects = await _projectService.GetAllProjectsAsync();
        if (!projects.Any())
        {
            Console.WriteLine("No projects found.");
            return;
        }

        Console.WriteLine("List of projects:");
        foreach (var project in projects)
        {
            Console.WriteLine($" {project.Id} - {project.ProjectName}, Status: {GetStatusName(project.StatusId)}");
        }

        int projectId = GetValidInt("Enter the ID of the project you want to update: ");
        var projectToUpdate = await _projectService.GetProjectByIdAsync(projectId);

        if (projectToUpdate == null)
        {
            Console.WriteLine("Project not found.");
            return;
        }

        Console.WriteLine($"Current name: {projectToUpdate.ProjectName}");
        Console.Write("New project name (leave empty to keep current): ");
        string? newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName)) projectToUpdate.ProjectName = newName;

        Console.WriteLine($"Current description: {projectToUpdate.Description}");
        Console.Write("New description (leave empty to keep current): ");
        string? newDescription = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDescription)) projectToUpdate.Description = newDescription;

        Console.WriteLine($"Current status: {GetStatusName(projectToUpdate.StatusId)}");
        int newStatus = GetValidInt("New status (1: Not Started, 2: In Progress, 3: Completed): ");
        projectToUpdate.StatusId = newStatus;

        Console.WriteLine($"Current estimated hours: {projectToUpdate.EstimatedHours}");
        projectToUpdate.EstimatedHours = GetValidDecimal("New estimated hours: ");

        Console.WriteLine($"Current hourly rate: {projectToUpdate.PricePerHour}");
        projectToUpdate.PricePerHour = GetValidDecimal("New hourly rate: ");

        if (newStatus == 3)
        {
            Console.Write("Enter end date (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                projectToUpdate.EndDate = endDate;
            }
            else
            {
                Console.WriteLine("Invalid date format");
            }
        }

        await _projectService.UpdateProjectAsync(projectToUpdate);
        Console.WriteLine("Project has been updated!");
    }
}
