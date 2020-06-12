using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PizzaShop.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        [ForeignKey ("AccountId")]
        public Account Account { get; set; }
        public long AccountId { get; set; }
    }
}
