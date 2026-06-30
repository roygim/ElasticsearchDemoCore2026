namespace ElasticsearchDemoCore2026.Interfaces;

public interface ICategoriesRepository
{
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
}
