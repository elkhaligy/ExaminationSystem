using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;
using ExaminationSystem.Core.Interfaces;
using System.Linq.Expressions;

namespace ExaminationSystem.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Instructor> _instructorRepository;
        private readonly IMapper _mapper;

        public DepartmentService(
            IGenericRepository<Department> departmentRepository,
            IGenericRepository<Student> studentRepository,
            IGenericRepository<Instructor> instructorRepository,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            return department != null ? _mapper.Map<DepartmentDto>(department) : null;
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto departmentDto)
        {
            // Validate department data
            if (string.IsNullOrWhiteSpace(departmentDto.Name))
                throw new ArgumentException("Department name cannot be empty", nameof(departmentDto));

            var department = _mapper.Map<Department>(departmentDto);
            _departmentRepository.Add(department);
            await _departmentRepository.SaveAsync();
            
            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto> UpdateDepartmentAsync(int id, UpdateDepartmentDto departmentDto)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
                throw new KeyNotFoundException($"Department with ID {id} not found");

            // Validate department data
            if (string.IsNullOrWhiteSpace(departmentDto.Name))
                throw new ArgumentException("Department name cannot be empty", nameof(departmentDto));

            _mapper.Map(departmentDto, existingDepartment);
            _departmentRepository.Update(existingDepartment);
            await _departmentRepository.SaveAsync();
            
            return _mapper.Map<DepartmentDto>(existingDepartment);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                throw new KeyNotFoundException($"Department with ID {id} not found");

            // Check if department has any students or instructors
            var hasStudents = await HasStudentsAsync(id);
            var hasInstructors = await HasInstructorsAsync(id);

            if (hasStudents || hasInstructors)
                throw new InvalidOperationException("Cannot delete department with associated students or instructors");

            await _departmentRepository.Delete(id);
            await _departmentRepository.SaveAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found");
            
            return _mapper.Map<IEnumerable<StudentDto>>(department.Students);
        }

        public async Task<IEnumerable<InstructorDto>> GetInstructorsByDepartmentAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found");

            return _mapper.Map<IEnumerable<InstructorDto>>(department.Instructors);
        }

        private async Task<bool> HasStudentsAsync(int departmentId)
        {
            Expression<Func<Student, bool>> predicate = s => s.DepartmentId == departmentId;
            var students = await _studentRepository.FindAsync(predicate);
            return students.Any();
        }

        private async Task<bool> HasInstructorsAsync(int departmentId)
        {
            Expression<Func<Instructor, bool>> predicate = i => i.DepartmentId == departmentId;
            var instructors = await _instructorRepository.FindAsync(predicate);
            return instructors.Any();
        }
    }
} 