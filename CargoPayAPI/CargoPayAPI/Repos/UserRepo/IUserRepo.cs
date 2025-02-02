using CargoPayAPI.DAL.Entities;

namespace CargoPayAPI.Repos.UserRepo
{
    public interface IUserRepo
    {
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUsername(string username);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(Guid id);
    }
}
