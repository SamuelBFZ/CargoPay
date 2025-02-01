using System.ComponentModel.DataAnnotations;

namespace CargoPayAPI.DAL.Entities
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Card Number")]
        [MaxLength(16, ErrorMessage = "Field {0} can´t have more than {1} characters")]
        [Required(ErrorMessage = "Field {0} can´t be empty")]
        public string Number { get; set; }

        [Display(Name = "Good Thru")]        
        [Required(ErrorMessage = "Field {0} can´t be empty")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/[0-9]{4}$", ErrorMessage = "Invalid format. Use MM/YYYY")] //Validation to save as only MM/YYYY format
        public string Expires { get; set; }

        [Display(Name = "CVV")]
        [MaxLength(4, ErrorMessage = "Field {0} can´t have more than {1} characters")]//Normally all cards have 3 CVV numbers, but American Express has 4
        [Required(ErrorMessage = "Field {0} can´t be empty")]
        public string Cvv { get; set; }

        [Display(Name = "Card Brand")]
        [MaxLength(100, ErrorMessage = "Field {0} can´t have more than {1} characters")]
        [Required(ErrorMessage = "Field {0} can´t be empty")]
        public string Brand { get; set; }

        [Display (Name = "Payments")]
        public ICollection<Payment> payments { get; set; }

    }
}
