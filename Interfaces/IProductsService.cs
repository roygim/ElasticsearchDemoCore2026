namespace DemoCore2026.Interfaces;

public interface IProductsService
{
    Task<ResponseObj<Product>> AddProductAsync(Product product);
    Task<ResponseObj<List<Product>>> SearchAsync(string query);
    Task<List<Product>> GetAllAsync();
    Task<ResponseObj<Product>> GetByIdAsync(int id);
}
