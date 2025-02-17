using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;


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
       

            // Retrieves the latest project number

            var lastProject = await _context.Projects
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();


          
            //If there are no projects, start on P - 1000
            if (lastProject == null || string.IsNullOrWhiteSpace(lastProject.ProjectNumber))
                return "P-1000";


    
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

             
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }


            public new async Task DeleteAsync(int id)
            {
            try
            {
                var project = await _context.Projects.FindAsync(id);
                if (project != null)
                {
                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Project successfully deleted.");
                }
                else
                {
                    Console.WriteLine("Project not found.");
                }
            }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting project: {ex.Message}");
                }
             }

        

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }


    }
}
