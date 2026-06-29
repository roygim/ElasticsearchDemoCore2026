namespace DemoCore2026.Services;

public class CategoriesService: ICategoriesService
{
    private readonly ICategoriesRepository _repository;
    private readonly IProductsRepository _productsRepository;

    public CategoriesService(ICategoriesRepository repository, IProductsRepository productsRepository)
    {
        _repository = repository;
        _productsRepository = productsRepository;
    }

    public async Task<ResponseObj<Category>> AddCategoryAsync(CreateCategoryDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id.Value);
        if (existing != null)
            return new ResponseObj<Category>
            {
                success = false,
                error = ErrorType.AlreadyExists,
                message = $"Category with id {dto.Id} already exists"
            };

        var name = dto.Name.Trim();
        var category = new Category
        {
            Id = dto.Id.Value,
            Name = char.ToUpper(name[0]) + name[1..]
        };

        await _repository.AddCategoryAsync(category);

        return new ResponseObj<Category> { success = true, data = category };
    }

    public async Task<ResponseObj<UpdateCategoryResult>> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
    {
        if (id < 0)
            return new ResponseObj<UpdateCategoryResult>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Category id must be zero or greater"
            };

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return new ResponseObj<UpdateCategoryResult>
            {
                success = false,
                error = ErrorType.NotFound,
                message = $"Category with id {id} was not found"
            };

        var name = dto.Name.Trim();
        var newName = char.ToUpper(name[0]) + name[1..];

        var updatedProductsCount = 0;
        if (existing.Name != newName)
        {
            existing.Name = newName;
            await _repository.UpdateCategoryAsync(existing);

            var products = await _productsRepository.GetByCategoryIdAsync(id);
            foreach (var product in products)
            {
                product.CategoryName = newName;
                await _productsRepository.UpdateProductAsync(product);
            }
            updatedProductsCount = products.Count;
        }

        return new ResponseObj<UpdateCategoryResult>
        {
            success = true,
            data = new UpdateCategoryResult
            {
                Category = existing,
                UpdatedProductsCount = updatedProductsCount
            }
        };
    }

    public async Task<ResponseObj<List<Category>>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return new ResponseObj<List<Category>> { success = true, data = categories };
    }

    public async Task<ResponseObj<Category>> GetByIdAsync(int id)
    {
        if (id < 0)
            return new ResponseObj<Category>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Category id must be zero or greater"
            };

        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            return new ResponseObj<Category>
            {
                success = false,
                error = ErrorType.NotFound,
                message = $"Category with id {id} was not found"
            };

        return new ResponseObj<Category> { success = true, data = category };
    }
}
