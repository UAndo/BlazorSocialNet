using BlazorSocialNet.Entities.Models;
using BlazorSocialNet.Repository;

namespace BlazorSocialNet.Business
{
    public class RoleManager : IRoleManager
    {
        private readonly IGenericRepository<Role> _roleRepository;

        public RoleManager(IGenericRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Add(Role role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task<bool> Delete(Role role)
        {
            return await _roleRepository.DeleteAsync(role);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetById(Guid id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<Role> GetByName(string name)
        {
            return await _roleRepository.GetSingleAsync("sp_Roles_GetIdByName",
                new { RoleName = name });
        }

        public async Task<bool> Update(Role role)
        {
            return await _roleRepository.UpdateAsync(role);
        }
    }
}
