using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity Create(ProjectModel model)
    {

        //Console.WriteLine($"ProjectFactory skapar entitet: {model.ProjectName});

        var project = new ProjectEntity
        {
          Id = model.Id,
            ProjectNumber = model.ProjectNumber,
            ProjectName = model.ProjectName,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            CustomerId = model.CustomerId,
            StatusId = model.StatusId,
            UserId = model.UserId,
            ProductId = model.ProductId,
            EstimatedHours = model.EstimatedHours ?? 0,
            PricePerHour = model.PricePerHour  ,
            TotalPrice = (model.PricePerHour ) * (model.EstimatedHours ?? 0)
        };

        // Beräkna TotalPrice
        if (model.EstimatedHours.HasValue && model.EstimatedHours > 0)
        {
            project.TotalPrice = (model.TotalPrice ?? 0m); 
        }

        return project;
    }

    public static ProjectModel CreateFromEntity(ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            ProjectNumber = entity.ProjectNumber,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerId = entity.CustomerId,
            StatusId = entity.StatusId,
            UserId = entity.UserId,
            ProductId = entity.ProductId,
            EstimatedHours = entity.EstimatedHours,
            PricePerHour = entity.PricePerHour ?? 0,
            TotalPrice = entity.TotalPrice
        };
    }

}
