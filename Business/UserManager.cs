using BlazorSocialNet.Entities.Models;
using BlazorSocialNet.Repository;

namespace BlazorSocialNet.Business
{
    public class UserManager : IUserManager
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserManager(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Add(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> Delete(User user)
        {
            return await _userRepository.DeleteAsync(user);
        }

        public async Task<bool> Update(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetSingleAsync<User>("sp_Users_GetByEmail", 
                new { Email = email });
        }

        public async Task<User> GetByToken(string token)
        {
            return await _userRepository.GetSingleAsync<User>("sp_Users_GetByToken", 
                new { Token = token });
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddToRole(Guid userId, Guid roleId)
        {
            return await _userRepository.PerformNonQueryAsync("sp_UserRoles_AddToRole", 
                new { UserId = userId, RoleId = roleId });
        }
    }
}