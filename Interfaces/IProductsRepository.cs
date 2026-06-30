namespace ElasticsearchDemoCore2026.Interfaces;

public interface IProductsRepository
{
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task<List<Product>> SearchAsync(string query);
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetByCategoryIdAsync(int categoryId);
    Task<Product?> GetByIdAsync(int id);
}