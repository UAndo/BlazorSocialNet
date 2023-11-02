using BlazorSocialNet.Entities.Models;

namespace BlazorSocialNet.Client.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>?> GetRoles();
        Task<Role?> GetRole(Guid roleId);
        Task<bool> CreateRole(Role role);
        Task<bool> UpdateRole(Guid roleId, Role role);
        Task<bool> DeleteRole(Guid roleId);
    }
}
