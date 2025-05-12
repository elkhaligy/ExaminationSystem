using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;
using ExaminationSystem.Core.Interfaces;
using System.Linq.Expressions;

namespace ExaminationSystem.Service
{
    public class ExamService : IExamService
    {
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IGenericRepository<Subject> _subjectRepository;
        private readonly IMapper _mapper;

        public ExamService(
            IGenericRepository<Exam> examRepository,
            IGenericRepository<Subject> subjectRepository,
            IMapper mapper)
        {
            _examRepository = examRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExamDto>> GetAllExamsAsync()
        {
            var exams = await _examRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task<ExamDto?> GetExamByIdAsync(int id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            return exam != null ? _mapper.Map<ExamDto>(exam) : null;
        }

        public async Task<ExamDto> CreateExamAsync(CreateExamDto examDto)
        {
            // Validate exam data
            if (string.IsNullOrWhiteSpace(examDto.Title))
                throw new ArgumentException("Exam title cannot be empty", nameof(examDto));

            if (examDto.SubjectId <= 0)
                throw new ArgumentException("Invalid subject ID", nameof(examDto));

            var subject = await _subjectRepository.GetByIdAsync(examDto.SubjectId);
            if (subject == null)
                throw new ArgumentException("Subject not found", nameof(examDto));

            if (examDto.StartTime >= examDto.EndTime)
                throw new ArgumentException("Start time must be before end time", nameof(examDto));

            if (examDto.Duration <= 0)
                throw new ArgumentException("Duration must be greater than 0", nameof(examDto));

            if (examDto.TotalMarks <= 0)
                throw new ArgumentException("Total marks must be greater than 0", nameof(examDto));

            if (examDto.PassingMarks <= 0 || examDto.PassingMarks > examDto.TotalMarks)
                throw new ArgumentException("Invalid passing marks", nameof(examDto));

            var exam = _mapper.Map<Exam>(examDto);
            _examRepository.Add(exam);
            await _examRepository.SaveAsync();
            
            return _mapper.Map<ExamDto>(exam);
        }

        public async Task<ExamDto> UpdateExamAsync(UpdateExamDto examDto)
        {
            var existingExam = await _examRepository.GetByIdAsync(examDto.Id);
            if (existingExam == null)
                throw new KeyNotFoundException($"Exam with ID {examDto.Id} not found");

            // Validate exam data
            if (string.IsNullOrWhiteSpace(examDto.Title))
                throw new ArgumentException("Exam title cannot be empty", nameof(examDto));

            if (examDto.SubjectId <= 0)
                throw new ArgumentException("Invalid subject ID", nameof(examDto));

            var subject = await _subjectRepository.GetByIdAsync(examDto.SubjectId);
            if (subject == null)
                throw new ArgumentException("Subject not found", nameof(examDto));

            if (examDto.StartTime >= examDto.EndTime)
                throw new ArgumentException("Start time must be before end time", nameof(examDto));

            if (examDto.Duration <= 0)
                throw new ArgumentException("Duration must be greater than 0", nameof(examDto));

            if (examDto.TotalMarks <= 0)
                throw new ArgumentException("Total marks must be greater than 0", nameof(examDto));

            if (examDto.PassingMarks <= 0 || examDto.PassingMarks > examDto.TotalMarks)
                throw new ArgumentException("Invalid passing marks", nameof(examDto));

            _mapper.Map(examDto, existingExam);
            _examRepository.Update(existingExam);
            await _examRepository.SaveAsync();
            
            return _mapper.Map<ExamDto>(existingExam);
        }

        public async Task DeleteExamAsync(int id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null)
                throw new KeyNotFoundException($"Exam with ID {id} not found");

            await _examRepository.Delete(id);
            await _examRepository.SaveAsync();
        }

        public async Task<IEnumerable<ExamDto>> GetExamsBySubjectAsync(int subjectId)
        {
            Expression<Func<Exam, bool>> predicate = e => e.SubjectId == subjectId;
            var exams = await _examRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task<IEnumerable<ExamDto>> GetActiveExamsAsync()
        {
            var now = DateTime.UtcNow;
            Expression<Func<Exam, bool>> predicate = e => 
                e.StartTime <= now && e.EndTime >= now;
            var exams = await _examRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public async Task<IEnumerable<ExamDto>> GetUpcomingExamsAsync()
        {
            var now = DateTime.UtcNow;
            Expression<Func<Exam, bool>> predicate = e => e.StartTime > now;
            var exams = await _examRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }
    }
}
