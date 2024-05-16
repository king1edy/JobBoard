using JobBoard.Dtos;
using JobBoard.Models;
using JobBoard.Services.Interface;
using JobBoard.Utility;

namespace JobBoard.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        string _containName = string.Empty;
        string _containName2 = string.Empty;

        public ApplicationService(ICosmosDbService cosmosDbService, IConfiguration configuration, ILogger<ApplicationService> logger)
        {
            _cosmosDbService = cosmosDbService;
            _configuration = configuration;
            _logger = logger;

            _containName = _configuration.GetSection("CosmoDB:ContainerName").Value;
            _containName2 = "ApplicationsContainer";
        }

        public async Task<ApplicationFormDto> SubmitApplication(ApplicationFormDto application)
        {
            try
            {
                ApplicationForm form = Common.MapDtoToApplicationForm(application);
                var resp = await _cosmosDbService.CreateItemAsync<ApplicationForm>(form, _containName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation(null, e, e.Message);
                throw;
            }

            return application;
        }

        public async Task<ApplicationFormDto> GetApplicationById(string id)
        {
            ApplicationFormDto formDto = new ApplicationFormDto();
            try
            {
                var resp = await _cosmosDbService.GetItemAsync<ApplicationForm>(id, "/application", _containName);
                if (resp is null)
                    return null;

                formDto = Common.MapDtoToApplicationForm(resp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogInformation(null, e, e.Message);
            }

            return formDto;
        }
    }
}
