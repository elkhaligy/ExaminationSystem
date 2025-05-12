using ExaminationSystem.Core.DTOs;

namespace ExaminationSystem.Core.Interfaces
{
    public interface IChoiceService
    {
        Task<IEnumerable<ChoiceDto>> GetAllChoicesAsync();
        Task<ChoiceDto?> GetChoiceByIdAsync(int id);
        Task<ChoiceDto> CreateChoiceAsync(CreateChoiceDto choiceDto);
        Task<ChoiceDto> UpdateChoiceAsync(int id, UpdateChoiceDto choiceDto);
        Task DeleteChoiceAsync(int id);
        Task<IEnumerable<ChoiceDto>> GetChoicesByQuestionAsync(int questionId);
    }
} 