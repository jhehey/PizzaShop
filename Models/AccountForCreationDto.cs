using System.ComponentModel.DataAnnotations;
namespace PizzaShop.Models
{
    public class AccountForCreationDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string ContactNumber { get; set; }
    }
}
