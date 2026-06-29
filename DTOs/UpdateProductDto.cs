using System.ComponentModel.DataAnnotations;

namespace DemoCore2026.DTOs
{
    public class UpdateProductDto
    {
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string? Name { get; set; }

        [Range(typeof(decimal), "0.01", "999999999999.99", ErrorMessage = "Price must be between 0.01 and 999999999999.99")]
        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }
    }
}
