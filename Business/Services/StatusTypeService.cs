using Business.Models;
using Business.Factories;
using Data.Interfaces;

namespace Business.Services
{
    public class StatusTypeService
    {
        private readonly IStatusTypeRepository _statusTypeRepository;

        public StatusTypeService(IStatusTypeRepository statusTypeRepository)
        {
            _statusTypeRepository = statusTypeRepository;
        }

        public async Task AddStatusTypeAsync(StatusTypeModel model)
        {
            var entity = StatusTypeFactory.Create(model);
            await _statusTypeRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<StatusTypeModel>> GetAllStatusTypesAsync()
        {
            var statusTypes = await _statusTypeRepository.GetAllAsync();
            return statusTypes.Select(StatusTypeFactory.Create);
        }

        public async Task<StatusTypeModel?> GetStatusTypeByIdAsync(int id)
        {
            var statusType = await _statusTypeRepository.GetByIdAsync(id);
            return statusType != null ? StatusTypeFactory.Create(statusType) : null;
        }

        public async Task UpdateStatusTypeAsync(StatusTypeModel model)
        {
            var entity = StatusTypeFactory.Create(model);
            await _statusTypeRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteStatusTypeAsync(int id)
        {
            return await _statusTypeRepository.DeleteAsync(id);
        }
    }
}
