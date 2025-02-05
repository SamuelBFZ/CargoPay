namespace CargoPayAPI.Models.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}
