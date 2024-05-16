using JobBoard.Dtos;
using JobBoard.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        //[HttpGet("get-all-questions")]
        //public async Task<IActionResult> GetAllQuestions()
        //{
        //    var questions = await _applicationService.GetAllQuestionsAsync();
        //    if (questions == null)
        //        return NotFound();

        //    return Ok(questions);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(string id)
        {
            var question = await _applicationService.GetApplicationById(id);
            if (question == null)
                return NotFound($"Application with ID {id} not found.");

            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationFormDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _applicationService.SubmitApplication(model);
            return CreatedAtAction(nameof(CreateApplication), model);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionDto questionDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var questionExists = await _applicationService.GetQuestionByIdAsync(id);
        //    if (questionExists == null)
        //        return NotFound($"Question with ID {id} not found.");

        //    await _applicationService.UpdateQuestionAsync(id, questionDto);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteQuestion(string id)
        //{
        //    var question = await _applicationService.GetQuestionByIdAsync(id);
        //    if (question == null)
        //        return NotFound($"Question with ID {id} not found.");

        //    await _applicationService.DeleteQuestionAsync(id);
        //    return NoContent();
        //}
    }
}
