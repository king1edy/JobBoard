using JobBoard.Dtos;

namespace JobBoard.Services.Interface
{
    public interface IQuestionService
    {
        Task<QuestionDto> GetQuestionByIdAsync(string id);
        Task<List<QuestionDto>> GetAllQuestionsAsync();
        Task CreateQuestionAsync(QuestionDto question);
        Task UpdateQuestionAsync(string id, QuestionDto question);
        Task DeleteQuestionAsync(string id);

        Task<List<string>> GetQuestionTypesAsync();
        Task<List<QuestionTypeDto>> GetAllQuestionTypesAsync();
        Task CreateQuestionTypeAsync(QuestionTypeDto questionType);
    }
}
