using HueFesAPI.Controllers;
using HueFesAPI.Data;
using HueFesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HueFesAPI.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public Account account = new Account();
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;


        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        // POST api/<AuthController>
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            var loginAccount = await _dbContext.Account.FirstOrDefaultAsync(a => a.Username == username
                                                                        && a.Password == AccountsController.MD5Hash(password));
            if (loginAccount == null)
            {
                return BadRequest("Invalid Username/Password");
            }

            var accountRole = await _dbContext.Role.FirstOrDefaultAsync(r => r.RoleId == loginAccount.RoleId);

            if (accountRole == null)
            {
                return BadRequest("Role not found in this account");
            }

            string token = CreateToken(loginAccount, accountRole.RoleName);

            var refeshToken = GenerateRefreshToken();
            SetRefreshToken(refeshToken, loginAccount);

            return Ok(new
            {
                Message = "Login success",
                Token = token,
                Role = accountRole.RoleName
            });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!account.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (account.ExpiresDate < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            var role = await _dbContext.Role.FirstOrDefaultAsync(r => r.RoleId ==  account.RoleId);
            string token = CreateToken(account, role.RoleName);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, account);

            return Ok(token);
        }

        private string CreateToken(Account account, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("JWT:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, Account account)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            account.RefreshToken = newRefreshToken.Token;
            account.DateCreated = newRefreshToken.Created;
            account.ExpiresDate = newRefreshToken.Expires;
        }
    }
}
