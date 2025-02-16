using Business.Models;
using Business.Factories;
using Data.Interfaces;

namespace Business.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(UserModel model)
        {
            var entity = UserFactory.Create(model);
            await _userRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(UserFactory.Create);
        }

        public async Task<UserModel?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? UserFactory.Create(user) : null;
        }

        public async Task UpdateUserAsync(UserModel model)
        {
            var entity = UserFactory.Create(model);
            await _userRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
