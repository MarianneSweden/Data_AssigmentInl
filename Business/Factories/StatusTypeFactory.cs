using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class StatusTypeFactory
    {
        public static StatusTypeEntity Create(StatusTypeModel model)
        {
            return new StatusTypeEntity
            {
                Id = model.Id,
                StatusName = model.StatusName
            };
        }

        public static StatusTypeModel Create(StatusTypeEntity entity)
        {
            return new StatusTypeModel
            {
                Id = entity.Id,
                StatusName = entity.StatusName
            };
        }
    }
}
