using Microsoft.Azure.Cosmos;
using System.ComponentModel;

namespace DynamicForms.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private Microsoft.Azure.Cosmos.Container _container;
    private CosmosClient _cosmosClient;
    private Database _database;
    private readonly IConfiguration configuration;

    public Repository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    private void InitializeCosmosClient()
    {
        var options = new CosmosClientOptions()
        {
            SerializerOptions = new CosmosSerializationOptions()
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            }
        };

        if (_cosmosClient == null)
        {
            _cosmosClient = new CosmosClient(configuration.GetConnectionString("CosmosDbConnectionString"),options);
        }
    }

    private async Task InitializeDatabase()
    {
        if (_database == null)
        {
            InitializeCosmosClient();
            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(configuration.GetSection("DatabaseSettings")["DatabaseName"]);
 
        }
    }

    private async Task InitializeContainer()
    {
        if (_container == null)
        {
            await InitializeDatabase();
            _container = await _database.CreateContainerIfNotExistsAsync(configuration.GetSection("DatabaseSettings")["ContainerName"], "/id");
           
        }
    }
    public async Task<T> GetByIdAsync(Guid id)
    {
        try
        {
            await InitializeContainer();
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        await InitializeContainer();
        var query = _container.GetItemQueryIterator<T>(new QueryDefinition("SELECT * FROM c"));
        var results = new List<T>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await InitializeContainer();
        var partitionKeyValue = GetPartitionKeyValue(entity);
        var response = await _container.CreateItemAsync(entity, new PartitionKey(partitionKeyValue));
        return response.Resource;
    }

    public async Task<T> UpdateAsync(Guid id, T entity)
    {
        await InitializeContainer();
        var response = await _container.ReplaceItemAsync(entity, id.ToString(), new PartitionKey(id.ToString()));
        return response.Resource;
    }

    public async Task DeleteAsync(Guid id)
    {
        await InitializeContainer();
        await _container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
    }

    private string GetPartitionKeyValue(T entity)
    {
        // Assuming PartitionKeyProperty is a property in your entity class
        var propertyInfo = typeof(T).GetProperty("Id");
        if (propertyInfo != null)
        {
            // Get the value of the partition key property from the entity
            var partitionKeyValue = propertyInfo.GetValue(entity)?.ToString();
            return partitionKeyValue;
        }

        // If the partition key property is not found or is null, return a default value or throw an exception
        throw new InvalidOperationException("Partition key value not found or null.");
    }
}
