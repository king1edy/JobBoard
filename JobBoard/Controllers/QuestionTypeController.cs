using JobBoard.Dtos;
using JobBoard.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionTypeController(IQuestionService questionService)
        {
            _questionService = questionService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionTypeById(string id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound($"Question with ID {id} not found.");

            return Ok(question);
        }

        [HttpGet("get-question-types")]
        public async Task<IActionResult> GetQuestionTypesAsync()
        {
            var questions = await _questionService.GetQuestionTypesAsync();
            if (questions == null)
                return NotFound();

            return Ok(questions);
        }


        [HttpGet("get-all-question-types")]
        public async Task<IActionResult> GetAllQuestionTypesAsync()
        {
            var questions = await _questionService.GetAllQuestionTypesAsync();
            if (questions == null)
                return NotFound();

            return Ok(questions);
        }


        [HttpPost]
        public async Task<IActionResult> CreateQuestionType([FromBody] QuestionTypeDto questionType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _questionService.CreateQuestionTypeAsync(questionType);
            return CreatedAtAction(nameof(CreateQuestionType), questionType);
        }
        
    }
}
