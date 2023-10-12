using BlazorSocialNet.Entities.Models.Authentication;
using BlazorSocialNet.Repository;

namespace BlazorSocialNet.Business
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> DeleteUser(User user)
        {
            return await _userRepository.DeleteAsync(user);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetSingleAsync<User>("sp_Users_GetByEmail", 
                new { Email = email });
        }

        public async Task<User> GetUserByToken(string token)
        {
            return await _userRepository.GetSingleAsync<User>("sp_Users_GetByToken", 
                new { Token = token });
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddToRoleAsync(Guid userId, Guid roleId)
        {
            return await _userRepository.PerformNonQueryAsync("sp_UserRoles_AddToRole", 
                new { UserId = userId, RoleId = roleId });
        }
    }
}