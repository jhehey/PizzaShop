using System;

namespace PizzaShop.Models
{
    public class AccountDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
