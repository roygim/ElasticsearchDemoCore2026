namespace DemoCore2026.Services;

public class ProductsService: IProductsService
{
    private readonly IProductsRepository _repository;

    public ProductsService(IProductsRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseObj<Product>> AddProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Product name is required"
            };

        var existing = await _repository.GetByIdAsync(product.Id);
        if (existing != null)
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.AlreadyExists,
                message = $"Product with id {product.Id} already exists"
            };

        await _repository.AddProductAsync(product);

        return new ResponseObj<Product> { success = true, data = product };
    }

    public async Task<List<Product>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<Product>();

        return await _repository.SearchAsync(query);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ResponseObj<Product>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Product id must be greater than zero"
            };

        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.NotFound,
                message = $"Product with id {id} was not found"
            };

        return new ResponseObj<Product> { success = true, data = product };
    }
}

