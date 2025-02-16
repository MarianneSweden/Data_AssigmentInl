using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository

    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}

