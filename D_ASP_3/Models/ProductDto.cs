using System.ComponentModel.DataAnnotations;

namespace D_ASP_3.Models
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
