using System.ComponentModel.DataAnnotations;

namespace CargoPayAPI.Models.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress] 
        public string Email { get; set;}

        [Required]
        public int Role {  get; set; }
    }
}
