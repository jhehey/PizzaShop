using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }

        [Column (TypeName = "Money")]
        public decimal Price { get; set; }

        [ForeignKey ("ShopId")]
        public Shop Shop { get; set; }
        public long ShopId { get; set; }
    }
}
