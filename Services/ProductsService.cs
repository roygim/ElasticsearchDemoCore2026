namespace DemoCore2026.Services;

public class ProductsService: IProductsService
{
    private readonly IProductsRepository _repository;

    public ProductsService(IProductsRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseObj<Product>> AddProductAsync(CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Product name is required"
            };

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing != null)
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.AlreadyExists,
                message = $"Product with id {dto.Id} already exists"
            };

        var product = new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            CategoryName = dto.CategoryName
        };

        await _repository.AddProductAsync(product);

        return new ResponseObj<Product> { success = true, data = product };
    }

    public async Task<ResponseObj<List<Product>>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Trim().Length < 2)
            return new ResponseObj<List<Product>>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Search query must be at least 2 characters"
            };

        var products = await _repository.SearchAsync(query);
        return new ResponseObj<List<Product>> { success = true, data = products };
    }

    public async Task<ResponseObj<List<Product>>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return new ResponseObj<List<Product>> { success = true, data = products };
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

