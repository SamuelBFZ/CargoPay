using System.ComponentModel.DataAnnotations;

namespace CargoPayAPI.Models.DTOs.User
{
    public class AuthenticateUserDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
