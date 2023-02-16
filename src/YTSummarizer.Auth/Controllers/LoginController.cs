using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using YTSummarizer.Dtos;
using YTSummarizer.Models;

namespace YTSummarizer.Auth.Controllers
{
    [ApiController]
    [Route("api/v1/authenticate")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await RegisterAsync(request);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        private async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null) return new RegisterResponse
            {
                Message = "User already exists",
                Success =
            false
            };
            //if we get here, no user with this email..
            userExists = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
            };
            var createUserResult = await _userManager.CreateAsync(userExists, request.Password);
            if (!createUserResult.Succeeded) return new RegisterResponse
            {
                Message = $"Create user failed{createUserResult?.Errors?.First()?.Description}",
                Success = false
            };

            return new RegisterResponse
            {
                Message = userExists.Email,
                Success = true
            };
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await LoginAsync(request);
            return result.Success ? Ok(result) : BadRequest(new { Success = result.Success, Message = result.Message });
        }

        private async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) return new LoginResponse { };
            var signInResponse = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!signInResponse.Succeeded)
            {
                return new LoginResponse
                {
                    Message = "The provided password is not correct.",
                    Success = false,
                };
            }
            //all is well if ew reach here
            var claims = new List<Claim>
                {
                new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim (ClaimTypes.Name, user.FullName),
                new Claim (ClaimTypes.Name, user.FullName),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString())
                };
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9xFRJkBVDjGZrg6Tkieq"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(30);
            var token = new JwtSecurityToken(
            issuer: "https://localhost:7012",
            audience: "https://localhost:7012",
            claims: claims,
            expires: expires,
            signingCredentials: creds
            );

            return new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Message = "Login Successful",
                Email = user.Email,
                UserId = user.Id.ToString(),
                Success = true,
            };
        }
    }
}