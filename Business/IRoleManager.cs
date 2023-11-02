using BlazorSocialNet.Entities.Models;

namespace BlazorSocialNet.Business
{
    public interface IRoleManager
    {
        Task<bool> Add(Role role);
        Task<bool> Update(Role role);
        Task<bool> Delete(Role role);
        Task<Role> GetById(Guid id);
        Task<Role> GetByName(string name);
        Task<IEnumerable<Role>> GetAll();
    }
}
