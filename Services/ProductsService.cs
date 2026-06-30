namespace ElasticsearchDemoCore2026.Services;

public class ProductsService: IProductsService
{
    private readonly IProductsRepository _repository;
    private readonly ICategoriesRepository _categoriesRepository;

    public ProductsService(IProductsRepository repository, ICategoriesRepository categoriesRepository)
    {
        _repository = repository;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<ResponseObj<Product>> AddProductAsync(CreateProductDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id.Value);
        if (existing != null)
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.AlreadyExists,
                message = $"Product with id {dto.Id} already exists"
            };

        int categoryId;
        string categoryName;
        if (dto.CategoryId == null)
        {
            categoryId = Constants.DefaultCategoryId;
            categoryName = Constants.DefaultCategoryName;
        }
        else
        {
            var category = await _categoriesRepository.GetByIdAsync(dto.CategoryId.Value);
            if (category == null)
                return new ResponseObj<Product>
                {
                    success = false,
                    error = ErrorType.ValidationError,
                    message = $"Category with id {dto.CategoryId} does not exist"
                };

            categoryId = category.Id;
            categoryName = category.Name;
        }

        var product = new Product
        {
            Id = dto.Id.Value,
            Name = dto.Name,
            Price = dto.Price.Value,
            CategoryId = categoryId,
            CategoryName = categoryName
        };

        await _repository.AddProductAsync(product);

        return new ResponseObj<Product> { success = true, data = product };
    }

    public async Task<ResponseObj<Product>> UpdateProductAsync(int id, UpdateProductDto dto)
    {
        if (id <= 0)
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Product id must be greater than zero"
            };

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return new ResponseObj<Product>
            {
                success = false,
                error = ErrorType.NotFound,
                message = $"Product with id {id} was not found"
            };

        if (dto.Name != null)
            existing.Name = dto.Name;

        if (dto.Price != null)
            existing.Price = dto.Price.Value;

        if (dto.CategoryId != null)
        {
            var category = await _categoriesRepository.GetByIdAsync(dto.CategoryId.Value);
            if (category == null)
                return new ResponseObj<Product>
                {
                    success = false,
                    error = ErrorType.ValidationError,
                    message = $"Category with id {dto.CategoryId} does not exist"
                };

            existing.CategoryId = category.Id;
            existing.CategoryName = category.Name;
        }

        await _repository.UpdateProductAsync(existing);

        return new ResponseObj<Product> { success = true, data = existing };
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

    public async Task<ResponseObj<List<Product>>> GetByCategoryIdAsync(int categoryId)
    {
        if (categoryId < 0)
            return new ResponseObj<List<Product>>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Category id must be zero or greater"
            };

        var products = await _repository.GetByCategoryIdAsync(categoryId);
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

