using System.Collections.Concurrent;
using JobBoard.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace JobBoard.Services
{
    public class CosmosDbService<T> : ICosmosDbService<T> where T : class
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;

        public CosmosDbService(string connectionString, string databaseName)
        {
            _cosmosClient = new CosmosClient(connectionString);
            _databaseName = databaseName;
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
