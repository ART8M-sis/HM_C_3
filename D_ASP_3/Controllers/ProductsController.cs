using D_ASP_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace D_ASP_3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Apple",  Price = 10.50m },
            new Product { Id = 2, Name = "Banana", Price = 7.20m  },
            new Product { Id = 3, Name = "Cherry", Price = 25.00m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll() => Ok(_products);

        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Create([FromBody] ProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required");

            var newProduct = new Product
            {
                Id = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1,
                Name = dto.Name,
                Price = dto.Price
            };

            _products.Add(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Product> Update(int id, [FromBody] ProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required");

            var exist = _products.FirstOrDefault(p => p.Id == id);
            if (exist == null) return NotFound("Product not found");

            exist.Name = dto.Name;
            exist.Price = dto.Price;
            return Ok(exist);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound("Product not found");

            _products.Remove(product);
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> Search([FromQuery] string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Query parameter 'name' is required");

            var results = _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            return Ok(results);
        }
    }
}