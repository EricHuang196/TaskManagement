using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IEnumerable<User>> GetAllAsync() => _userRepository.GetAllAsync();

        public Task<User?> GetByIdAsync(int id) => _userRepository.GetByIdAsync(id);

        public Task<int> CreateAsync(User user)
        {
            user.CreatedAt = DateTime.Now;
            return _userRepository.CreateAsync(user);
        }

        public Task<bool> UpdateAsync(User user) => _userRepository.UpdateAsync(user);

        public Task<bool> DeleteAsync(int id) => _userRepository.DeleteAsync(id);
    }
}
