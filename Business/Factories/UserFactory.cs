using Business.Models;
using Data.Entities;

namespace Business.Factories
{

    public static class UserFactory
    {

        public static UserEntity Create(UserModel model)
        {
            return new UserEntity
            {
                Id = model.Id,
                FirstName = model.FirstName,  
                LastName = model.LastName,    
                Email = model.Email           
            };
        }



        public static UserModel Create(UserEntity entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }
    }

}
