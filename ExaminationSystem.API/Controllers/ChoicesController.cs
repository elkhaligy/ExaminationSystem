using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoicesController : ControllerBase
    {
        private readonly IChoiceService _choiceService;

        public ChoicesController(IChoiceService choiceService)
        {
            _choiceService = choiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChoiceDto>>> GetChoices()
        {
            var choices = await _choiceService.GetAllChoicesAsync();
            return Ok(choices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChoiceDto>> GetChoice(int id)
        {
            var choice = await _choiceService.GetChoiceByIdAsync(id);
            if (choice == null)
            {
                return NotFound();
            }
            return Ok(choice);
        }

        [HttpPost]
        public async Task<ActionResult<ChoiceDto>> CreateChoice(CreateChoiceDto choiceDto)
        {
            try
            {
                var createdChoice = await _choiceService.CreateChoiceAsync(choiceDto);
                return CreatedAtAction(nameof(GetChoice), new { id = createdChoice.Id }, createdChoice);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChoice(int id, UpdateChoiceDto choiceDto)
        {
            try
            {
                await _choiceService.UpdateChoiceAsync(id, choiceDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            try
            {
                await _choiceService.DeleteChoiceAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<ChoiceDto>>> GetChoicesByQuestion(int questionId)
        {
            var choices = await _choiceService.GetChoicesByQuestionAsync(questionId);
            return Ok(choices);
        }
    }
} 