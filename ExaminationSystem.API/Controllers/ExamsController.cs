using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetExams()
        {
            var exams = await _examService.GetAllExamsAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDto>> GetExam(int id)
        {
            var exam = await _examService.GetExamByIdAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(exam);
        }

        [HttpPost]
        public async Task<ActionResult<ExamDto>> CreateExam(CreateExamDto examDto)
        {
            try
            {
                var createdExam = await _examService.CreateExamAsync(examDto);
                return CreatedAtAction(nameof(GetExam), new { id = createdExam.Id }, createdExam);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, UpdateExamDto examDto)
        {
            if (id != examDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _examService.UpdateExamAsync(examDto);
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
        public async Task<IActionResult> DeleteExam(int id)
        {
            try
            {
                await _examService.DeleteExamAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetExamsBySubject(int subjectId)
        {
            var exams = await _examService.GetExamsBySubjectAsync(subjectId);
            return Ok(exams);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetActiveExams()
        {
            var exams = await _examService.GetActiveExamsAsync();
            return Ok(exams);
        }

        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetUpcomingExams()
        {
            var exams = await _examService.GetUpcomingExamsAsync();
            return Ok(exams);
        }
    }
} 