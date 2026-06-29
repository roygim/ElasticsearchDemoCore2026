using System.ComponentModel.DataAnnotations;

namespace DemoCore2026.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Id is required and must be greater than zero")]
        [Range(1, int.MaxValue, ErrorMessage = "Id is required and must be greater than zero")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required and must be at least 2 characters")]
        [MinLength(2, ErrorMessage = "Name is required and must be at least 2 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required and must be greater than zero")]
        [Range(0, double.MaxValue, MinimumIsExclusive = true, ErrorMessage = "Price is required and must be greater than zero")]
        public double? Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
