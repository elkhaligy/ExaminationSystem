using ExaminationSystem.Core.DTOs;

namespace ExaminationSystem.Core.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto studentDto);
        Task<StudentDto> UpdateStudentAsync(UpdateStudentDto studentDto);
        Task DeleteStudentAsync(int id);
        Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId);
        Task<IEnumerable<StudentExamDto>> GetStudentExamsAsync(int studentId);
    }
} 