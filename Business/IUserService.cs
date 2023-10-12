using BlazorSocialNet.Entities.Models.Authentication;

namespace BlazorSocialNet.Business
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<User> GetUserById(Guid id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByToken(string token);
        Task<bool> AddToRoleAsync(Guid userId, Guid roleId);
    }
}
