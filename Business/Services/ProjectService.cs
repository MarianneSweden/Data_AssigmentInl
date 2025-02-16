using Business.Models;
using Business.Factories;
using Data.Interfaces;

namespace Business.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task AddProjectAsync(ProjectModel model)
        {
            try
            {
                var entity = ProjectFactory.Create(model);


                // Generera unikt projektnummer
                entity.ProjectNumber = await _projectRepository.GenerateNextProjectNumberAsync();

                // Beräkna TotalPrice
                entity.TotalPrice = (entity.PricePerHour ?? 0) * (entity.EstimatedHours ?? 0);

                await _projectRepository.AddAsync(entity);

                //Console.WriteLine($"Försöker spara: {entity.ProjectName}");

                await _projectRepository.AddAsync(entity);

                Console.WriteLine($"Projekt {entity.ProjectName} har sparats i databasen!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparande av projekt: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(ProjectFactory.CreateFromEntity);
        }

        public async Task UpdateProjectAsync(ProjectModel model)
        {
            try
            {
                var entity = await _projectRepository.GetByIdAsync(model.Id);
                if (entity != null)
                {
                    entity.ProjectName = model.ProjectName;
                    entity.Description = model.Description;
                    entity.StatusId = model.StatusId;
                    entity.EndDate = model.EndDate;
                    entity.EstimatedHours = model.EstimatedHours;
                    entity.PricePerHour = model.PricePerHour;

                    // Beräkna totalpris
                    decimal estimatedHours = entity.EstimatedHours ?? 0;
                    decimal pricePerHour = entity.PricePerHour ?? 0;
                    entity.TotalPrice = estimatedHours * pricePerHour;

                    Console.WriteLine($"Uppdaterar projekt: {entity.ProjectName}, Nytt TotalPrice: {entity.TotalPrice}");

                    await _projectRepository.UpdateAsync(entity);

                    Console.WriteLine($"Projekt {entity.ProjectName} har uppdaterats!");
                }
                else
                {
                    Console.WriteLine($"Projekt med ID {model.Id} hittades inte.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid uppdatering av projekt: {ex.Message}");
            }
        }

        public async Task<ProjectModel?> GetProjectByIdAsync(int id)
        {
            var entity = await _projectRepository.GetByIdAsync(id);
            return entity != null ? ProjectFactory.CreateFromEntity(entity) : null;
        }
    }
}
