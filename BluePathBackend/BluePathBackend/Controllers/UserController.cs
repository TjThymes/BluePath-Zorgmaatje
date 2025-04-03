using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BluePath_Backend.Objects;
using BluePath_Backend.Interfaces;
using BluePathBackend.Other;

using BluePathBackend.Interfaces;
using BluePathBackend.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BluePathBackend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenGenerator _jwtTokenGenerator;


        public AuthController(IUserRepository userRepository, IConfiguration configuration, UserManager<ApplicationUser> userManager, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest(new { Message = "Gebruiker bestaat al." });

            var user = new ApplicationUser
            {
                Fullname = model.Fullname,
                Email = model.Email,
                UserName = model.Email,
                AccountType = model.AccountType
            };

            var result = await _userRepository.CreateAsync(user, model.Password);
            if (!result)
                return BadRequest(new { Message = "Registratie mislukt." });

            return Ok(new { Message = "Gebruiker geregistreerd." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized(new { Message = "Ongeldige gebruikersnaam of wachtwoord." });
                }

                var isValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isValid)
                {
                    return Unauthorized(new { Message = "Ongeldige gebruikersnaam of wachtwoord." });
                }

                var token = _jwtTokenGenerator.GenerateToken(user);
                return Ok(new { accessToken = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Interne serverfout", Error = ex.Message });
            }
        }


        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("test-auth")]
        public IActionResult TestAuth()
        {
            return Ok(new
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}

