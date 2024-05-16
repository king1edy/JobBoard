using JobBoard.Dtos;
using JobBoard.Services.Interface;

namespace JobBoard.Services
{
    public class QuestionService : IQuestionService
    {
        public async Task<QuestionDto> GetQuestionByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionDto>> GetAllQuestionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateQuestionAsync(QuestionDto question)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuestionAsync(string id, QuestionDto question)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteQuestionAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetQuestionTypesAsync()
        {
            throw new NotImplementedException();
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
