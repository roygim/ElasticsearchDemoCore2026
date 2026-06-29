using Elastic.Clients.Elasticsearch;

namespace DemoCore2026.Repositories;

public class ProductsRepository: IProductsRepository
{
    private readonly ElasticsearchClient _client;
    private const string IndexName = "products";

    public ProductsRepository()
    {
        _client = ElasticsearchClientFactory.Create();
    }

    public async Task AddProductAsync(Product product)
    {
        var response = await _client.IndexAsync(product, i => i
           .Index("products")
           .Id(product.Id.ToString())
           .OpType(OpType.Create)
        );

        if (!response.IsValidResponse)
        {
            throw new Exception("Product already exists or indexing failed");
        }
    }

    public async Task<List<Product>> SearchAsync(string query)
    {
        var response = await _client.SearchAsync<Product>(s => s
            .Index(IndexName)
            .Query(q => q
                .Bool(b => b
                    .Should(
                        sh => sh.Wildcard(w => w
                            .Field(f => f.Name)
                            .Value($"*{query}*")
                            .CaseInsensitive(true)),
                        sh => sh.Wildcard(w => w
                            .Field(f => f.CategoryName)
                            .Value($"*{query}*")
                            .CaseInsensitive(true))
                    )
                    .MinimumShouldMatch(1)
                )
            )
        );

        return response.Hits
            .Select(h => h.Source)
            .Where(x => x != null)
            .ToList()!;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var response = await _client.SearchAsync<Product>(s => s
            .Index(IndexName)
            .Size(1000)
            .Query(q => q.MatchAll(m => { }))
        );

        return response.Hits
            .Select(h => h.Source)
            .Where(x => x != null)
            .ToList()!;
    }

    public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
    {
        var response = await _client.SearchAsync<Product>(s => s
            .Index(IndexName)
            .Size(1000)
            .Query(q => q
                .Term(t => t.Field(f => f.CategoryId).Value(categoryId))
            )
        );

        return response.Hits
            .Select(h => h.Source)
            .Where(x => x != null)
            .ToList()!;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync<Product>(id.ToString(), g => g
            .Index(IndexName)
        );

        return response.Source;
    }
}
