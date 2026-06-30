using System.ComponentModel.DataAnnotations;

namespace ElasticsearchDemoCore2026.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Id is required and must be greater than zero")]
        [Range(1, int.MaxValue, ErrorMessage = "Id is required and must be greater than zero")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required and must be at least 2 characters")]
        [MinLength(2, ErrorMessage = "Name is required and must be at least 2 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(typeof(decimal), "0.01", "999999999999.99", ErrorMessage = "Price must be between 0.01 and 999999999999.99")]
        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }
    }
}
