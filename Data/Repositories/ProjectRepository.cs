using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Threading.Tasks;

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
            var lastProject = await _context.Projects
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            // Om det inte finns några projekt, börja på P-1000, fungerar inte just nu
            if (lastProject == null || string.IsNullOrWhiteSpace(lastProject.ProjectNumber))
                return "P-1000";

            // Kontrollera att formatet är korrekt innan vi försöker splitta
            var parts = lastProject.ProjectNumber.Split('-');
            if (parts.Length < 2 || !int.TryParse(parts[1], out int lastNumber))
            {
                Console.WriteLine("Felaktigt projektnummerformat, återgår till P-1000");
                return "P-1000";
            }

            // Öka med 1
            int nextNumber = lastNumber + 1;
            return $"P-{nextNumber}";
        }

        public override async Task AddAsync(ProjectEntity entity)
        {
            try
            {
                Console.WriteLine($"Försöker spara i databasen: {entity.ProjectName} ");

                await _context.Projects.AddAsync(entity);
                await _context.SaveChangesAsync();

                Console.WriteLine("Projekt sparat i databasen!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande i databasen: {ex.Message}");

                // Skriv ut inner exception om den finns
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

    }
}
