using System.ComponentModel.DataAnnotations;

namespace ElasticsearchDemoCore2026.DTOs
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Name is required and must be at least 2 characters")]
        [MinLength(2, ErrorMessage = "Name is required and must be at least 2 characters")]
        public string Name { get; set; }
    }
}
