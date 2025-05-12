using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetSubject(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubject(CreateSubjectDto subjectDto)
        {
            try
            {
                var createdSubject = await _subjectService.CreateSubjectAsync(subjectDto);
                return CreatedAtAction(nameof(GetSubject), new { id = createdSubject.Id }, createdSubject);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto subjectDto)
        {
            if (id != subjectDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _subjectService.UpdateSubjectAsync(subjectDto);
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
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjectsByInstructor(int instructorId)
        {
            var subjects = await _subjectService.GetSubjectsByInstructorAsync(instructorId);
            return Ok(subjects);
        }
    }
} 