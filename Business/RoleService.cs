using BlazorSocialNet.Entities.Models.Authorization;
using BlazorSocialNet.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSocialNet.Business
{
    public class RoleService : IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;

        public RoleService(IGenericRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> AddRole(Role role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task<bool> DeleteRole(Role role)
        {
            return await _roleRepository.DeleteAsync(role);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<Role> GetRoleByName(string name)
        {
            return await _roleRepository.GetSingleAsync("sp_Roles_GetIdByName",
                new { RoleName = name });
        }

        public async Task<bool> UpdateRole(Role role)
        {
            return await _roleRepository.UpdateAsync(role);
        }
    }
}
