using JobBoard.Dtos;
using JobBoard.Models;
using JobBoard.Services.Interface;

namespace JobBoard.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        string _containName = string.Empty;

        public ApplicationService(ICosmosDbService cosmosDbService, IConfiguration configuration)
        {
            _cosmosDbService = cosmosDbService;
            _configuration = configuration;

            _containName = _configuration.GetSection("CosmoDB:ContainerName").Value;
        }

        public async Task<ApplicationFormDto> SubmitApplication(ApplicationFormDto application)
        {
            try
            {
                ApplicationForm form = new ApplicationForm()
                {
                    Id = Guid.NewGuid().ToString(),
                    FormId = Guid.NewGuid().ToString(),
                    ProgramTitle = application.ProgramTitle,
                    ProgramDescription = application.ProgramDescription,
                    Questions = application.Questions.Select(q => new Question()
                    {
                        Id = Guid.NewGuid().ToString(),
                        IsRequired = q.IsRequired,
                        Content = q.Content,
                        Type = new QuestionType()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Type = q.Type.Type,
                        },
                        Options = q.Options.Select(op => new QuestionOption()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Choice = op.Option
                        }).ToList(),
                    }).ToList(),
                    AdditionalQuestions = application.AdditionalQuestions.Select(q => new Question()
                    {
                        Id = Guid.NewGuid().ToString(),
                        IsRequired = q.IsRequired,
                        Content = q.Content,
                        Type = new QuestionType()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Type = q.Type.Type,
                        },
                        Options = q.Options.Select(o => new QuestionOption()
                        {
                            Id = o.Id,
                            Choice = o.Option
                            
                        }).ToList()
                    }).ToList()
                };
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

                formDto = new ApplicationFormDto()
                {
                    ProgramTitle = resp.ProgramTitle,
                    ProgramDescription = resp.ProgramDescription,
                    Questions = resp.Questions.Select(q => new QuestionDto()
                    {
                        Id = q.Id,
                        Content = q.Content,
                        Type = new QuestionTypeDto()
                        {
                            Id = q.Type.Id,
                            Type = q.Type.Type,
                        },
                        IsRequired = q.IsRequired,
                        Options = q.Options.Select(o => new QuestionOptionDto()
                        {
                            Id = o.Id,
                            Option = o.Choice
                        }).ToList()
                    }).ToList(),
                    AdditionalQuestions = resp.AdditionalQuestions.Select(q => new QuestionDto()
                    {
                        Id = q.Id,
                        Content = q.Content,
                        Type = new QuestionTypeDto()
                        {
                            Id = q.Type.Id,
                            Type = q.Type.Type,
                        },
                        IsRequired = q.IsRequired,
                        Options = q.Options.Select(o => new QuestionOptionDto()
                        {
                            Id = o.Id,
                            Option = o.Choice
                        }).ToList()
                    }).ToList()
                };
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
