using ExaminationSystem.Core.DTOs;

namespace ExaminationSystem.Core.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync();
        Task<InstructorDto?> GetInstructorByIdAsync(int id);
        Task<InstructorDto> CreateInstructorAsync(CreateInstructorDto instructorDto);
        Task<InstructorDto> UpdateInstructorAsync(UpdateInstructorDto instructorDto);
        Task DeleteInstructorAsync(int id);
        Task<IEnumerable<InstructorDto>> GetInstructorsByDepartmentAsync(int departmentId);
    }
} 