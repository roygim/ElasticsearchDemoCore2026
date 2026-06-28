namespace DemoCore2026.Interfaces;

public interface IProductsService
{
    Task AddProductAsync(Product product);
    Task<List<Product>> SearchAsync(string query);
    Task<Product?> GetByIdAsync(int id);
}
