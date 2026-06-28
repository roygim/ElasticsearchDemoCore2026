namespace DemoCore2026.Interfaces;

public interface ICategoriesService
{
    Task<ResponseObj<Category>> AddCategoryAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<ResponseObj<Category>> GetByIdAsync(int id);
}
