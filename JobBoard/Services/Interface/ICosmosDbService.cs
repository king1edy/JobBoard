namespace JobBoard.Services.Interface
{
    public interface ICosmosDbService
    {
        Task<T> CreateItemAsync<T>(T item, string containerName);
        Task<T> UpdateItemAsync<T>(string id, T item, string containerName);
        Task<T> GetItemAsync<T>(string id, string partitionKey, string containerName);
        Task<bool> DeleteItemAsync(string id, string containerName);
    }
}
