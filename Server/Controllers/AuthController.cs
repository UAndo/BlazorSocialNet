using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlazorSocialNet.Business;
using BlazorSocialNet.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlazorSocialNet.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userService;
        private readonly IRoleManager _roleService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserManager userService, IRoleManager roleService, IConfiguration configuration)
        {
            _userService = userService;
            _roleService = roleService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            try
            {
                var user = await _userService.GetByEmail(request.Email);
                if (user != null)
                    return BadRequest("User already exists.");

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                user = new User()
                {
                    Id = new Guid(),
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    VerificationToken = CreateRandomToken()
                };

                var result = await _userService.Add(user);
                if (!result)
                    return BadRequest("Internal server error.");

                var role = await _roleService.GetByName("User");

                await _userService.AddToRole(user.Id, role.Id);

                return Ok("User successfully created!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _userService.GetByEmail(request.Email);
            if (user == null)
            {
                return BadRequest("Email or password is incorrect.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Email or password is incorrect.");
            }

            if (user.VerifiedAt == null)
            {
                return BadRequest("Not verified!");
            }

            string token = CreateJwToken(user);

            return Ok(token);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var user = await _userService.GetByToken(token);
            if (user == null)
            {
                return BadRequest("Invalid token.");
            }

            user.VerifiedAt = DateTime.UtcNow;
            await _userService.Update(user);

            return Ok("User verified! :)");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userService.GetByEmail(email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _userService.Update(user);
            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userService.GetByToken(request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.PasswordHash = passwordHash;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _userService.Update(user);

            return Ok("Password successfully reset.");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private string CreateJwToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
