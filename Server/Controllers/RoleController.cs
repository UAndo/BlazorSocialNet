using BlazorSocialNet.Business;
using BlazorSocialNet.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSocialNet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IGenericRepository<IRoleService> _roleRepository;
    }
}
