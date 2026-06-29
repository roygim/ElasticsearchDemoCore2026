using Elastic.Clients.Elasticsearch;

namespace DemoCore2026.Repositories;

public class CategoriesRepository: ICategoriesRepository
{
    private readonly ElasticsearchClient _client;
    private const string IndexName = "categories";

    public CategoriesRepository()
    {
        _client = ElasticsearchClientFactory.Create();
    }

    public async Task AddCategoryAsync(Category category)
    {
        var response = await _client.IndexAsync(category, i => i
           .Index(IndexName)
           .Id(category.Id.ToString())
           .OpType(OpType.Create)
        );

        if (!response.IsValidResponse)
        {
            throw new Exception("Category already exists or indexing failed");
        }
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        var response = await _client.IndexAsync(category, i => i
           .Index(IndexName)
           .Id(category.Id.ToString())
        );

        if (!response.IsValidResponse)
        {
            throw new Exception("Category update failed");
        }
    }

    public async Task<List<Category>> GetAllAsync()
    {
        var response = await _client.SearchAsync<Category>(s => s
            .Index(IndexName)
            .Size(1000)
            .Query(q => q.MatchAll(m => { }))
        );

        return response.Hits
            .Select(h => h.Source)
            .Where(x => x != null)
            .ToList()!;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync<Category>(id.ToString(), g => g
            .Index(IndexName)
        );

        return response.Source;
    }
}
