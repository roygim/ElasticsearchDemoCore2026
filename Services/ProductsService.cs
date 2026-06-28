namespace DemoCore2026.Services;

public class ProductsService: IProductsService
{
    private readonly IProductsRepository _repository;

    public ProductsService(IProductsRepository repository)
    {
        _repository = repository;
    }

    public async Task AddProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required");

        await _repository.IndexAsync(product);
    }

    public async Task<List<Product>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<Product>();

        return await _repository.SearchAsync(query);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        if (id <= 0)
            return null;

        return await _repository.GetByIdAsync(id);
    }
}

