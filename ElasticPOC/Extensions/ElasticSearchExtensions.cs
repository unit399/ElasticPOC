using ElasticPOC.Models;
using Nest;
using Newtonsoft.Json;

namespace ElasticPOC.Extensions;

public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["ELKConfiguration:Uri"] ?? "http://localhost:9200";
        var defaultIndex = configuration["ELKConfiguration:DefaultIndex"] ?? "default-index";
        var deleteAndReSeed = configuration["ELKConfiguration:DeleteAndReseed"] == "true";

        var settings = new ConnectionSettings(new Uri(url)).PrettyJson().DefaultIndex(defaultIndex);

        AddDefaultMappings(settings);

        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);

        CreateIndex(client, defaultIndex, deleteAndReSeed);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        /* For ignoring properties in the mapping
         settings.DefaultMappingFor<Product>(p =>
            p.Ignore(x => x.Price)
                .Ignore(x => x.Id)
                .Ignore(x => x.Quantity));
        */
    }

    private static void CreateIndex(IElasticClient client, string indexName, bool deleteAndReSeed = false)
    {
        if (string.IsNullOrWhiteSpace(indexName))
            return;

        if (deleteAndReSeed)
        {
            client.Indices.Delete(indexName);
            client.Indices.Create(indexName, i => i.Map<Product>(m => m.AutoMap()));

            var data = LoadProductsFromFile("seedData.json");
            if (data == null) return;

            foreach (var product in data)
                client.IndexDocumentAsync(product);
        }
        else
        {
            client.Indices.Create(indexName, i => i.Map<Product>(m => m.AutoMap()));
        }
    }

    private static IEnumerable<Product>? LoadProductsFromFile(string fileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        var json = File.ReadAllText(path);
        var products = JsonConvert.DeserializeObject<List<Product>>(json);

        return products;
    }
}