using BlazorSocialNet.Business;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSocialNet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userService;

        public UserController(IUserManager userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);  
            }
            
        }
    }
}
