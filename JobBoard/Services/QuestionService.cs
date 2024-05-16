using System.Reflection;
using Azure.Core;
using JobBoard.Dtos;
using JobBoard.Models;
using JobBoard.Services.Interface;
using JobBoard.Utility;
using Microsoft.Azure.Cosmos;

namespace JobBoard.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        string _containName = string.Empty;

        public QuestionService(ICosmosDbService cosmosDbService, IConfiguration configuration, ILogger logger)
        {
            _cosmosDbService = cosmosDbService;
            _configuration = configuration;
            _logger = logger;

            _containName = _configuration.GetSection("CosmoDB:ContainerName").Value;
        }


        public async Task<IEnumerable<Question>> GetAllItemsAsync()
        {
            List<Question> results = new List<Question>();
            var container = _cosmosDbService.GetContainerAsync(_containName).Result;
             var query = container.GetItemQueryIterator<Question>(new QueryDefinition("SELECT * FROM c"));
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange((IEnumerable<Question>)response.ToList());
            }
            return results;
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(string id)
        {
            QuestionDto questionDto = new QuestionDto();
            try
            {
                var resp = await _cosmosDbService.GetItemAsync<Question>(id, "/question", _containName);
                if (resp is null)
                    return null;

                questionDto = Common.MapQuesstionToDto(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogInformation(null, ex, ex.Message);
                throw;
            }
            return questionDto;
        }

        public async Task<List<QuestionDto>> GetAllQuestionsAsync()
        {
            List<QuestionDto> questionDtos = new List<QuestionDto>();

            try
            {
                var questions = await GetAllItemsAsync();
                questionDtos = questions.Select(q => new QuestionDto()
                {
                    Id = q.Id,
                    IsRequired = q.IsRequired,
                    Content = q.Content,
                    Type = new QuestionTypeDto()
                    {
                        Id = q.Type.Id,
                        Type = q.Type.Type,
                    },
                    Options = q.Options.Select(o => new QuestionOptionDto()
                    {
                        Id = o.Id,
                        Option = o.Choice
                    }).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return questionDtos;
        }

        public async Task CreateQuestionAsync(QuestionDto model)
        {
            try
            {
                var question = Common.MapDtoToQuestion(model);
                await _cosmosDbService.CreateItemAsync<Question>(question, _containName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task UpdateQuestionAsync(string id, QuestionDto model)
        {
            try
            {
                var question = Common.MapDtoToQuestion(model);
                await _cosmosDbService.UpdateItemAsync<Question>(id, question, _containName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task DeleteQuestionAsync(string id)
        {
            try
            {
                await _cosmosDbService.DeleteItemAsync(id, _containName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<List<string>> GetQuestionTypesAsync()
        {
            return Enum.GetNames(typeof(QuestionTypeEnum)).ToList();
        }

        public async Task<List<QuestionTypeDto>> GetAllQuestionTypesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateQuestionTypeAsync(QuestionTypeDto questionType)
        {
            throw new NotImplementedException();
        }
    }
}
