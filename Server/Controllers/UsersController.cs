using BlazorSocialNet.Business;
using BlazorSocialNet.Entities.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSocialNet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all-users")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return new List<User>
            {
                new User("test@gmail.com", "Andrew", "en"),
                new User("alicia@gmail.com", "Alicia", "en"),
                new User("ira@gmail.com", "Ira", "uk"),
                new User("john@gmail.com", "John", "en"),
            };
        }
    }
}
