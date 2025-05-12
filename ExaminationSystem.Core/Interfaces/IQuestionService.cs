using ExaminationSystem.Core.DTOs;

namespace ExaminationSystem.Core.Interfaces
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync();
        Task<QuestionDto?> GetQuestionByIdAsync(int id);
        Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto questionDto);
        Task<QuestionDto> UpdateQuestionAsync(int id, UpdateQuestionDto questionDto);
        Task DeleteQuestionAsync(int id);
        Task<IEnumerable<QuestionDto>> GetQuestionsByExamAsync(int examId);

        Task<IEnumerable<ChoiceDto>> GetChoicesByQuestion(int questionId);

    }
} 