using JobBoard.Dtos;
using JobBoard.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("get-all-questions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            if (questions == null)
                return NotFound();

            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(string id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound($"Question with ID {id} not found.");

            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _questionService.CreateQuestionAsync(questionDto);
            return CreatedAtAction(nameof(CreateQuestion), questionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var questionExists = await _questionService.GetQuestionByIdAsync(id);
            if (questionExists == null)
                return NotFound($"Question with ID {id} not found.");

            await _questionService.UpdateQuestionAsync(id, questionDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound($"Question with ID {id} not found.");

            await _questionService.DeleteQuestionAsync(id);
            return NoContent();
        }
    }
}
