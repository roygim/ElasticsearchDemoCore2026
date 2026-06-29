namespace DemoCore2026.Interfaces;

public interface ICategoriesService
{
    Task<ResponseObj<Category>> AddCategoryAsync(CreateCategoryDto dto);
    Task<ResponseObj<List<Category>>> GetAllAsync();
    Task<ResponseObj<Category>> GetByIdAsync(int id);
}
