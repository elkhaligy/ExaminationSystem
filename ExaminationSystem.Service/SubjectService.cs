using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;
using ExaminationSystem.Core.Interfaces;
using System.Linq.Expressions;

namespace ExaminationSystem.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<Subject> _subjectRepository;
        private readonly IGenericRepository<Instructor> _instructorRepository;
        private readonly IMapper _mapper;

        public SubjectService(
            IGenericRepository<Subject> subjectRepository,
            IGenericRepository<Instructor> instructorRepository,
            IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _instructorRepository = instructorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }

        public async Task<SubjectDto?> GetSubjectByIdAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            return subject != null ? _mapper.Map<SubjectDto>(subject) : null;
        }

        public async Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto)
        {
            // Validate subject data
            if (string.IsNullOrWhiteSpace(subjectDto.Name))
                throw new ArgumentException("Subject name cannot be empty", nameof(subjectDto));

            if (subjectDto.InstructorId <= 0)
                throw new ArgumentException("Invalid instructor ID", nameof(subjectDto));

            var instructor = await _instructorRepository.GetByIdAsync(subjectDto.InstructorId);
            if (instructor == null)
                throw new ArgumentException("Instructor not found", nameof(subjectDto));

            var subject = _mapper.Map<Subject>(subjectDto);
            _subjectRepository.Add(subject);
            await _subjectRepository.SaveAsync();
            
            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task<SubjectDto> UpdateSubjectAsync(UpdateSubjectDto subjectDto)
        {
            var existingSubject = await _subjectRepository.GetByIdAsync(subjectDto.Id);
            if (existingSubject == null)
                throw new KeyNotFoundException($"Subject with ID {subjectDto.Id} not found");

            // Validate subject data
            if (string.IsNullOrWhiteSpace(subjectDto.Name))
                throw new ArgumentException("Subject name cannot be empty", nameof(subjectDto));

            if (subjectDto.InstructorId <= 0)
                throw new ArgumentException("Invalid instructor ID", nameof(subjectDto));

            var instructor = await _instructorRepository.GetByIdAsync(subjectDto.InstructorId);
            if (instructor == null)
                throw new ArgumentException("Instructor not found", nameof(subjectDto));

            _mapper.Map(subjectDto, existingSubject);
            _subjectRepository.Update(existingSubject);
            await _subjectRepository.SaveAsync();
            
            return _mapper.Map<SubjectDto>(existingSubject);
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
                throw new KeyNotFoundException($"Subject with ID {id} not found");

            await _subjectRepository.Delete(id);
            await _subjectRepository.SaveAsync();
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjectsByInstructorAsync(int instructorId)
        {
            Expression<Func<Subject, bool>> predicate = s => s.InstructorId == instructorId;
            var subjects = await _subjectRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }
    }
} 