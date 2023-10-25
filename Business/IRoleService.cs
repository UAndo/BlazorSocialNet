using BlazorSocialNet.Entities.Models.Authorization;

namespace BlazorSocialNet.Business
{
    public interface IRoleService
    {
        Task<bool> AddRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(Role role);
        Task<Role> GetRoleById(Guid id);
        Task<Guid> GetRoleByName(string name);
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
