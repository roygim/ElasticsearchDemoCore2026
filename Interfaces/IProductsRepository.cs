namespace DemoCore2026.Interfaces;

public interface IProductsRepository
{
    Task AddProductAsync(Product product);
    Task<List<Product>> SearchAsync(string query);
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
}