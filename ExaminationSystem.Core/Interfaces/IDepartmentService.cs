using ExaminationSystem.Core.DTOs;

namespace ExaminationSystem.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto departmentDto);
        Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto departmentDto);
        Task DeleteDepartmentAsync(int id);
        Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId);
        Task<IEnumerable<InstructorDto>> GetInstructorsByDepartmentAsync(int departmentId);
    }
} 