using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;
using ExaminationSystem.Core.Interfaces;
using System.Linq.Expressions;

namespace ExaminationSystem.Service
{
    public class InstructorService : IInstructorService
    {
        private readonly IGenericRepository<Instructor> _instructorRepository;
        private readonly IGenericRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public InstructorService(
            IGenericRepository<Instructor> instructorRepository,
            IGenericRepository<Department> departmentRepository,
            IMapper mapper)
        {
            _instructorRepository = instructorRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync()
        {
            var instructors = await _instructorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InstructorDto>>(instructors);
        }

        public async Task<InstructorDto?> GetInstructorByIdAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            return instructor != null ? _mapper.Map<InstructorDto>(instructor) : null;
        }

        public async Task<InstructorDto> CreateInstructorAsync(CreateInstructorDto instructorDto)
        {
            // Validate instructor data
            if (string.IsNullOrWhiteSpace(instructorDto.Name))
                throw new ArgumentException("Instructor name cannot be empty", nameof(instructorDto));

            if (instructorDto.DepartmentId <= 0)
                throw new ArgumentException("Invalid department ID", nameof(instructorDto));

            var department = await _departmentRepository.GetByIdAsync(instructorDto.DepartmentId);
            if (department == null)
                throw new ArgumentException("Department not found", nameof(instructorDto));

            var instructor = _mapper.Map<Instructor>(instructorDto);
            _instructorRepository.Add(instructor);
            await _instructorRepository.SaveAsync();
            
            return _mapper.Map<InstructorDto>(instructor);
        }

        public async Task<InstructorDto> UpdateInstructorAsync(UpdateInstructorDto instructorDto)
        {
            var existingInstructor = await _instructorRepository.GetByIdAsync(instructorDto.Id);
            if (existingInstructor == null)
                throw new KeyNotFoundException($"Instructor with ID {instructorDto.Id} not found");

            // Validate instructor data
            if (string.IsNullOrWhiteSpace(instructorDto.Name))
                throw new ArgumentException("Instructor name cannot be empty", nameof(instructorDto));

            if (instructorDto.DepartmentId <= 0)
                throw new ArgumentException("Invalid department ID", nameof(instructorDto));

            var department = await _departmentRepository.GetByIdAsync(instructorDto.DepartmentId);
            if (department == null)
                throw new ArgumentException("Department not found", nameof(instructorDto));

            _mapper.Map(instructorDto, existingInstructor);
            _instructorRepository.Update(existingInstructor);
            await _instructorRepository.SaveAsync();
            
            return _mapper.Map<InstructorDto>(existingInstructor);
        }

        public async Task DeleteInstructorAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor == null)
                throw new KeyNotFoundException($"Instructor with ID {id} not found");

            await _instructorRepository.Delete(id);
            await _instructorRepository.SaveAsync();
        }

        public async Task<IEnumerable<InstructorDto>> GetInstructorsByDepartmentAsync(int departmentId)
        {
            Expression<Func<Instructor, bool>> predicate = i => i.DepartmentId == departmentId;
            var instructors = await _instructorRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<InstructorDto>>(instructors);
        }
    }
} 