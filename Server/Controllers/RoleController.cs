using BlazorSocialNet.Business;
using BlazorSocialNet.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSocialNet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IRoleManager _roleRepository;

        public RoleController(IRoleManager roleRepository) {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles() {
            try 
            {
                var roles = await _roleRepository.GetAll();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            try
            {
                var role = await _roleRepository.GetById(id);
                return Ok(role);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            try 
            {
                if (role is null)
                    return BadRequest();
            
                var result = await _roleRepository.Add(role);

                if (result)
                    return Ok();
                else 
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            try 
            {
                var result = await _roleRepository.Update(role);
                if (result)
                    return NoContent();
                else 
                    return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Role role)
        {
            try
            {
                var roleToDelete = await _roleRepository.Delete(role);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
