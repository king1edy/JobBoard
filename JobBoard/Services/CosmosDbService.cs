using System.Collections.Concurrent;
using JobBoard.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace JobBoard.Services
{
    public class CosmosDbService: ICosmosDbService
    {
        private readonly IConfiguration _configuration;
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly string _connectionString;

        public CosmosDbService(string databaseName, IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionString = _configuration.GetSection("CosmoDB:ConnectionString").Value;
            _cosmosClient = new CosmosClient(_connectionString);
            _databaseName = _configuration.GetSection("CosmoDB:DatabaseName").Value;
        }

        public async Task<T> CreateItemAsync<T>(T item, string containerName)
        {
            var container = _cosmosClient.GetContainer(_databaseName, containerName);
            var response = await container.CreateItemAsync(item);
            return response.Resource;
        }

        public async Task<T> UpdateItemAsync<T>(string id, T item, string containerName)
        {
            var container = _cosmosClient.GetContainer(_databaseName, containerName);
            var response = await container.ReplaceItemAsync(item, id);
            return response.Resource;
        }

        public async Task<T> GetItemAsync<T>(string id, string partitionKey, string containerName)
        {
            var container = _cosmosClient.GetContainer(_databaseName, containerName);
            try
            {
                var response = await container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        public async Task<bool> DeleteItemAsync(string id, string containerName)
        {
            var container = _cosmosClient.GetContainer(_databaseName, containerName);
            try
            {
                var response = await container.DeleteItemAsync<object>(id, new PartitionKey(id));
                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }
    }
}
