using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data.Repositories
{
    public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
    {
        private new readonly DataContext _context;

        public ProjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<string> GenerateNextProjectNumberAsync()
        {
            // Hämtar senaste projektnumret
            // Retrieves the latest project number

            var lastProject = await _context.Projects
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            // Om det inte finns några projekt, börja på P-1000
            //If there are no projects, start on P - 1000
            if (lastProject == null || string.IsNullOrWhiteSpace(lastProject.ProjectNumber))
                return "P-1000";

            // Kontrollera att formatet är korrekt innan vi försöker splitta
            //Check that the format is correct before we attempt to split
            var parts = lastProject.ProjectNumber.Split('-');
            if (parts.Length < 2 || !int.TryParse(parts[1], out int lastNumber))
            {
                Console.WriteLine("Incorrect project number format, reverting to P-1000");
                return "P-1000";
            }

            // Increase by 1
            int nextNumber = lastNumber + 1;
            return $"P-{nextNumber}";
        }

        public override async Task AddAsync(ProjectEntity entity)
        {
            try
            {
               // Console.WriteLine($"Försöker spara i databasen: {entity.ProjectName} ");

                await _context.Projects.AddAsync(entity);
                await _context.SaveChangesAsync();

                Console.WriteLine("Project saved in the database!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when saving to the database: {ex.Message}");

                // Print inner exception if present
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }


    }
}
