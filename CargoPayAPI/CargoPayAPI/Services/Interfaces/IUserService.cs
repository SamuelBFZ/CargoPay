using CargoPayAPI.DAL.Entities;

namespace CargoPayAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(Guid id);
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
        Task<string> HashPassword(string password);
    }
}
