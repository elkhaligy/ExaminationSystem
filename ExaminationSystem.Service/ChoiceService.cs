using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;
using ExaminationSystem.Core.Interfaces;
using System.Linq.Expressions;

namespace ExaminationSystem.Service
{
    public class ChoiceService : IChoiceService
    {
        private readonly IGenericRepository<Choice> _choiceRepository;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IMapper _mapper;

        public ChoiceService(
            IGenericRepository<Choice> choiceRepository,
            IGenericRepository<Question> questionRepository,
            IMapper mapper)
        {
            _choiceRepository = choiceRepository;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChoiceDto>> GetAllChoicesAsync()
        {
            var choices = await _choiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ChoiceDto>>(choices);
        }

        public async Task<ChoiceDto?> GetChoiceByIdAsync(int id)
        {
            var choice = await _choiceRepository.GetByIdAsync(id);
            return choice != null ? _mapper.Map<ChoiceDto>(choice) : null;
        }

        public async Task<ChoiceDto> CreateChoiceAsync(CreateChoiceDto choiceDto)
        {
            // Validate choice data
            if (string.IsNullOrWhiteSpace(choiceDto.Text))
                throw new ArgumentException("Choice text cannot be empty", nameof(choiceDto));

            if (choiceDto.QuestionId <= 0)
                throw new ArgumentException("Invalid question ID", nameof(choiceDto));

            var question = await _questionRepository.GetByIdAsync(choiceDto.QuestionId);
            if (question == null)
                throw new ArgumentException("Question not found", nameof(choiceDto));

            var choice = _mapper.Map<Choice>(choiceDto);
            _choiceRepository.Add(choice);
            await _choiceRepository.SaveAsync();
            
            return _mapper.Map<ChoiceDto>(choice);
        }

        public async Task<ChoiceDto> UpdateChoiceAsync(int id, UpdateChoiceDto choiceDto)
        {
            var existingChoice = await _choiceRepository.GetByIdAsync(id);
            if (existingChoice == null)
                throw new KeyNotFoundException($"Choice with ID {id} not found");

            // Validate choice data
            if (string.IsNullOrWhiteSpace(choiceDto.Text))
                throw new ArgumentException("Choice text cannot be empty", nameof(choiceDto));

            _mapper.Map(choiceDto, existingChoice);
            _choiceRepository.Update(existingChoice);
            await _choiceRepository.SaveAsync();
            
            return _mapper.Map<ChoiceDto>(existingChoice);
        }

        public async Task DeleteChoiceAsync(int id)
        {
            var choice = await _choiceRepository.GetByIdAsync(id);
            if (choice == null)
                throw new KeyNotFoundException($"Choice with ID {id} not found");

            await _choiceRepository.Delete(id);
            await _choiceRepository.SaveAsync();
        }

        public async Task<IEnumerable<ChoiceDto>> GetChoicesByQuestionAsync(int questionId)
        {
            Expression<Func<Choice, bool>> predicate = c => c.QuestionId == questionId;
            var choices = await _choiceRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<ChoiceDto>>(choices);
        }
    }
} 