using CargoPayAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CargoPayAPI.DAL.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "Field {0} can´t have more than {1} characters")]
        public string Username { get; set;}

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set;}

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set;}

        public string? RefreshToken { get; set;}

        public DateTime? RefreshTokenExpiryTime { get; set;} 
        public UserRole Role { get; set;}

        [Display (Name = "Cards")]
        [JsonIgnore]
        public ICollection<Card> cards { get; set; } = new List<Card>();
    }
}
