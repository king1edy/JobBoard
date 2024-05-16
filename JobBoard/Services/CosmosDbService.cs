using JobBoard.Services.Interface;

namespace JobBoard.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        public async Task<T> CreateItemAsync<T>(T item, string containerName)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateItemAsync<T>(string id, T item, string containerName)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetItemAsync<T>(string id, string partitionKey, string containerName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(string id, string containerName)
        {
            throw new NotImplementedException();
        }
    }
}
