using CargoPayAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CargoPayAPI.DAL.Entities
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Payment Amount")]
        [Required(ErrorMessage = "Field {0} can´t be empty")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Payment Status")]
        [Required(ErrorMessage = "Field {0} can´t be empty")]
        public PaymentStatus Status { get; set; }

        [Display(Name = "Card Id")]
        public Card Card { get; set; }
    }
}
