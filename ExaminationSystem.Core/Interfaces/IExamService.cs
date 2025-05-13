using ExaminationSystem.Core.DTOs;

namespace ExaminationSystem.Core.Interfaces
{
    public interface IExamService
    {
        Task<IEnumerable<ExamDto>> GetAllExamsAsync();
        Task<ExamDto?> GetExamByIdAsync(int id);
        Task<ExamDto> CreateExamAsync(CreateExamDto examDto);
        Task<ExamDto> UpdateExamAsync(UpdateExamDto examDto);
        Task DeleteExamAsync(int id);
        Task<IEnumerable<ExamDto>> GetExamsBySubjectAsync(int subjectId);
        Task<IEnumerable<ExamDto>> GetActiveExamsAsync();
        Task<IEnumerable<ExamDto>> GetUpcomingExamsAsync();

        Task<IEnumerable<QuestionDto>> GetQuestions(int examId);

    }
} 