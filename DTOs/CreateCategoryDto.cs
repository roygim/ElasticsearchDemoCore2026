using System.ComponentModel.DataAnnotations;

namespace DemoCore2026.DTOs
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Id is required and must be greater than zero")]
        [Range(1, int.MaxValue, ErrorMessage = "Id is required and must be greater than zero")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Name is required and must be at least 2 characters")]
        [MinLength(2, ErrorMessage = "Name is required and must be at least 2 characters")]
        public string Name { get; set; }
    }
}
