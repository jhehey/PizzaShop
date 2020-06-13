using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Entities
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        [ForeignKey ("AccountId")]
        public Account Account { get; set; }
        public long AccountId { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product> ();
    }
}
