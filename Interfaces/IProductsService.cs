namespace DemoCore2026.Interfaces;

public interface IProductsService
{
    Task<ResponseObj<Product>> AddProductAsync(CreateProductDto dto);
    Task<ResponseObj<Product>> UpdateProductAsync(int id, UpdateProductDto dto);
    Task<ResponseObj<List<Product>>> SearchAsync(string query);
    Task<ResponseObj<List<Product>>> GetAllAsync();
    Task<ResponseObj<List<Product>>> GetByCategoryIdAsync(int categoryId);
    Task<ResponseObj<Product>> GetByIdAsync(int id);
}
