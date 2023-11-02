using BlazorSocialNet.Entities.Models;

namespace BlazorSocialNet.Business
{
    public interface IUserManager
    {
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(User user);
        Task<User> GetById(Guid id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmail(string email);
        Task<User> GetByToken(string token);
        Task<bool> AddToRole(Guid userId, Guid roleId);
    }
}
