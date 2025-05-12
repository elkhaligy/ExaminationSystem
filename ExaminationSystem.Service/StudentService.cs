using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;
using ExaminationSystem.Core.Interfaces;
using System.Linq.Expressions;

namespace ExaminationSystem.Service
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<StudentExam> _studentExamRepository;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public StudentService(
            IGenericRepository<Student> studentRepository,
            IGenericRepository<StudentExam> studentExamRepository,
            IGenericRepository<Department> departmentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _studentExamRepository = studentExamRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student != null ? _mapper.Map<StudentDto>(student) : null;
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto studentDto)
        {
            // Validate student data
            if (string.IsNullOrWhiteSpace(studentDto.Name))
                throw new ArgumentException("Student name cannot be empty", nameof(studentDto));

            if (studentDto.DepartmentId <= 0)
                throw new ArgumentException("Invalid department ID", nameof(studentDto));

            var department = await _departmentRepository.GetByIdAsync(studentDto.DepartmentId);
            if (department == null)
                throw new ArgumentException("Department not found", nameof(studentDto));

            var student = _mapper.Map<Student>(studentDto);
            _studentRepository.Add(student);
            await _studentRepository.SaveAsync();
            
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> UpdateStudentAsync(UpdateStudentDto studentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(studentDto.Id);
            if (existingStudent == null)
                throw new KeyNotFoundException($"Student with ID {studentDto.Id} not found");

            // Validate student data
            if (string.IsNullOrWhiteSpace(studentDto.Name))
                throw new ArgumentException("Student name cannot be empty", nameof(studentDto));

            if (studentDto.DepartmentId <= 0)
                throw new ArgumentException("Invalid department ID", nameof(studentDto));

            var department = await _departmentRepository.GetByIdAsync(studentDto.DepartmentId);
            if (department == null)
                throw new ArgumentException("Department not found", nameof(studentDto));

            _mapper.Map(studentDto, existingStudent);
            _studentRepository.Update(existingStudent);
            await _studentRepository.SaveAsync();
            
            return _mapper.Map<StudentDto>(existingStudent);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found");

            await _studentRepository.Delete(id);
            await _studentRepository.SaveAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId)
        {
            Expression<Func<Student, bool>> predicate = s => s.DepartmentId == departmentId;
            var students = await _studentRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<IEnumerable<StudentExamDto>> GetStudentExamsAsync(int studentId)
        {
            Expression<Func<StudentExam, bool>> predicate = se => se.StudentId == studentId;
            var studentExams = await _studentExamRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<StudentExamDto>>(studentExams);
        }
    }
} 