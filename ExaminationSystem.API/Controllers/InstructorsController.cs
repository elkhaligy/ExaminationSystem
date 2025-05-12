using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructors()
        {
            var instructors = await _instructorService.GetAllInstructorsAsync();
            return Ok(instructors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorDto>> GetInstructor(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return Ok(instructor);
        }

        [HttpPost]
        public async Task<ActionResult<InstructorDto>> CreateInstructor(CreateInstructorDto instructorDto)
        {
            try
            {
                var createdInstructor = await _instructorService.CreateInstructorAsync(instructorDto);
                return CreatedAtAction(nameof(GetInstructor), new { id = createdInstructor.Id }, createdInstructor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstructor(int id, UpdateInstructorDto instructorDto)
        {
            if (id != instructorDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _instructorService.UpdateInstructorAsync(instructorDto);
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
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            try
            {
                await _instructorService.DeleteInstructorAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructorsByDepartment(int departmentId)
        {
            var instructors = await _instructorService.GetInstructorsByDepartmentAsync(departmentId);
            return Ok(instructors);
        }
    }
} 