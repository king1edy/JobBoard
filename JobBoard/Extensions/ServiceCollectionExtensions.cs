using JobBoard.Services;
using JobBoard.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace AppForm.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("CosmoDB:ConnectionString").Value;
            string dbName = configuration.GetSection("CosmoDB:DatabaseName").Value;
            string containerName = configuration.GetSection("CosmoDB:ContainerName").Value;

            //services.AddSingleton<CosmoDbContext>(s =>
            //    new CosmoDbContext(connectionString, dbName, containerName));

            services.AddSingleton<ICosmosDbService, CosmosDbService>();

            services.AddSingleton<IQuestionService, QuestionService>();
            services.AddSingleton<IApplicationService, ApplicationService>();

            return services;
        }

        public static IServiceCollection AddCosmosClient(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton(ctx =>
            {
                string serviceEndpoint = configuration.GetSection("CosmosDb:ServiceEndpoint").Value;
                string authKey = configuration.GetSection("CosmosDb:Key").Value;

                return new CosmosClient(serviceEndpoint, authKey, new CosmosClientOptions
                {
                    SerializerOptions = new CosmosSerializationOptions
                    {
                        IgnoreNullValues = false,
                        Indented = false,
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                    }
                });
            });
    }
}