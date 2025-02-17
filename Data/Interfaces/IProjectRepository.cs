using Data.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        Task<string> GenerateNextProjectNumberAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }

}
