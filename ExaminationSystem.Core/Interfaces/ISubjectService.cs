using ExaminationSystem.Core.DTOs;

namespace ExaminationSystem.Core.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync();
        Task<SubjectDto?> GetSubjectByIdAsync(int id);
        Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto);
        Task<SubjectDto> UpdateSubjectAsync(UpdateSubjectDto subjectDto);
        Task DeleteSubjectAsync(int id);
        Task<IEnumerable<SubjectDto>> GetSubjectsByInstructorAsync(int instructorId);
    }
} 