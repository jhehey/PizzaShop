using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Contexts;
using PizzaShop.Entities;

namespace PizzaShop.Controllers
{

    [ApiController]
    [Route ("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly PizzaShopDbContext context;
        public ProductsController (PizzaShopDbContext context)
        {
            this.context = context ??
                throw new ArgumentNullException (nameof (context));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts ()
        {
            var products = await context.Products.ToListAsync ();
            return Ok (products);
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Product>> GetProductById (long id)
        {
            var product = await context.Products.FirstOrDefaultAsync (p => p.Id == id);
            if (product == null) return NotFound ();
            return Ok (product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct ([FromBody] Product product)
        {
            context.Products.Add (product);
            await context.SaveChangesAsync ();
            return CreatedAtAction (nameof (GetProductById), new { id = product.Id }, product);
        }
    }
}
