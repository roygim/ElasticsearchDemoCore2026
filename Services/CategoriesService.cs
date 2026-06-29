namespace DemoCore2026.Services;

public class CategoriesService: ICategoriesService
{
    private readonly ICategoriesRepository _repository;

    public CategoriesService(ICategoriesRepository repository)
    {
        _repository = repository;
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

    public async Task<ResponseObj<Category>> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
    {
        if (id < 0)
            return new ResponseObj<Category>
            {
                success = false,
                error = ErrorType.ValidationError,
                message = "Category id must be zero or greater"
            };

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return new ResponseObj<Category>
            {
                success = false,
                error = ErrorType.NotFound,
                message = $"Category with id {id} was not found"
            };

        var name = dto.Name.Trim();
        existing.Name = char.ToUpper(name[0]) + name[1..];

        await _repository.UpdateCategoryAsync(existing);

        return new ResponseObj<Category> { success = true, data = existing };
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
