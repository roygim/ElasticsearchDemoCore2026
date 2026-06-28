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

    public async Task IndexAsync(Product product)
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
            .Index("products")
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Name)
                    .Query(query)
                )
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
