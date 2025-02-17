using Business.Models;
using Business.Factories;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

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
            using var transaction = await _projectRepository.BeginTransactionAsync();
            try
            {
                var entity = ProjectFactory.Create(model);


                //Generate unique project number
                entity.ProjectNumber = await _projectRepository.GenerateNextProjectNumberAsync();

                //Calculate TotalPrice
                entity.TotalPrice = (entity.PricePerHour ?? 0) * (entity.EstimatedHours ?? 0);

               

                await _projectRepository.AddAsync(entity);
                await transaction.CommitAsync();

               // Console.WriteLine($"Project '{entity.ProjectName}' has been saved to the database!
             
                 Console.WriteLine($"Save project: {entity.ProjectName}, ProjectNumber: {entity.ProjectNumber}, TotalPrice: {entity.TotalPrice}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error saving project, transaction rolled back: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(ProjectFactory.CreateFromEntity);
        }

        public async Task UpdateProjectAsync(ProjectModel model)
        {
            using var transaction = await _projectRepository.BeginTransactionAsync();
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

                    // Calculte total price
                    decimal estimatedHours = entity.EstimatedHours ?? 0;
                    decimal pricePerHour = entity.PricePerHour ?? 0;
                    entity.TotalPrice = estimatedHours * pricePerHour;

                    Console.WriteLine($"Updating project: {entity.ProjectName}, New TotalPrice: {entity.TotalPrice}");

                    await _projectRepository.UpdateAsync(entity);
                    await transaction.CommitAsync();

                    Console.WriteLine($"Project '{entity.ProjectName}' has been updated!");
                }
                else
                {
                    Console.WriteLine($"Project with ID {model.Id} was not found.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error updating project, transaction rolled back: {ex.Message}");
            }
        }

        public async Task<ProjectModel?> GetProjectByIdAsync(int id)
        {
            var entity = await _projectRepository.GetByIdAsync(id);
            return entity != null ? ProjectFactory.CreateFromEntity(entity) : null;
        }
    }
}
