namespace DemoCore2026.Services;

public class CategoriesService: ICategoriesService
{
    private readonly ICategoriesRepository _repository;

    public CategoriesService(ICategoriesRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseObj<Category>> AddCategoryAsync(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            return new ResponseObj<Category>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Category name is required"
            };

        var existing = await _repository.GetByIdAsync(category.Id);
        if (existing != null)
            return new ResponseObj<Category>
            {
                success = false,
                error = ErrorType.AlreadyExists,
                message = $"Category with id {category.Id} already exists"
            };

        await _repository.AddCategoryAsync(category);

        return new ResponseObj<Category> { success = true, data = category };
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
