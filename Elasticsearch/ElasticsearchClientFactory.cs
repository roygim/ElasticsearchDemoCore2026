using Elastic.Clients.Elasticsearch;

namespace DemoCore2026.Elasticsearch;

public static class ElasticsearchClientFactory
{
    public static ElasticsearchClient Create()
    {
        var settings = new ElasticsearchClientSettings(
            new Uri("http://localhost:9200"))
            .DefaultIndex("products");

        return new ElasticsearchClient(settings);
    }
}