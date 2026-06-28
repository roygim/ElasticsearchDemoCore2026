namespace DemoCore2026.Interfaces;

public interface IProductsRepository
{
    Task IndexAsync(Product product);
    Task<List<Product>> SearchAsync(string query);
    Task<Product?> GetByIdAsync(int id);
}