using CargoPayAPI.DAL.Entities;
using CargoPayAPI.Repos.UserRepo;
using CargoPayAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CargoPayAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepo userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                return await _userRepo.CreateUserAsync(user);
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.InnerException?.Message ?? exception.Message);
            }

        }

        public async Task<string> HashPassword(string password)
        {
            return await Task.Run (() => BCrypt.Net.BCrypt.HashPassword(password));
        }

        private bool VerifyPasswordHash(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepo.GetUserById(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepo.GetUserByUsername(username);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepo.UpdateUserAsync(user);
        }

        public async Task<User> DeleteUserAsync(Guid id)
        {
            return await _userRepo.DeleteUserAsync(id);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepo.GetUserByUsername(username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                return new AuthenticationResult { Success = false, Error = "Invalid credentials" };
            }

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); //7 days to expire token

            await _userRepo.UpdateUserAsync(user);

            return new AuthenticationResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}
