using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;
using ExaminationSystem.Core.Interfaces;
using System.Linq.Expressions;

namespace ExaminationSystem.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IMapper _mapper;

        public QuestionService(
            IGenericRepository<Question> questionRepository,
            IGenericRepository<Exam> examRepository,
            IMapper mapper)
        {
            _questionRepository = questionRepository;
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<QuestionDto?> GetQuestionByIdAsync(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            return question != null ? _mapper.Map<QuestionDto>(question) : null;
        }

        public async Task<QuestionDto> CreateQuestionAsync(CreateQuestionDto questionDto)
        {
            // Validate question data
            if (string.IsNullOrWhiteSpace(questionDto.Text))
                throw new ArgumentException("Question text cannot be empty", nameof(questionDto));

            if (questionDto.ExamId <= 0)
                throw new ArgumentException("Invalid exam ID", nameof(questionDto));

            var exam = await _examRepository.GetByIdAsync(questionDto.ExamId);
            if (exam == null)
                throw new ArgumentException("Exam not found", nameof(questionDto));

            var question = _mapper.Map<Question>(questionDto);
            _questionRepository.Add(question);
            await _questionRepository.SaveAsync();
            
            return _mapper.Map<QuestionDto>(question);
        }

        public async Task<QuestionDto> UpdateQuestionAsync(int id, UpdateQuestionDto questionDto)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(id);
            if (existingQuestion == null)
                throw new KeyNotFoundException($"Question with ID {id} not found");

            // Validate question data
            if (string.IsNullOrWhiteSpace(questionDto.Text))
                throw new ArgumentException("Question text cannot be empty", nameof(questionDto));

            _mapper.Map(questionDto, existingQuestion);
            _questionRepository.Update(existingQuestion);
            await _questionRepository.SaveAsync();
            
            return _mapper.Map<QuestionDto>(existingQuestion);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
                throw new KeyNotFoundException($"Question with ID {id} not found");

            await _questionRepository.Delete(id);
            await _questionRepository.SaveAsync();
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsByExamAsync(int examId)
        {
            Expression<Func<Question, bool>> predicate = q => q.ExamId == examId;
            var questions = await _questionRepository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<IEnumerable<ChoiceDto>> GetChoicesByQuestion(int questionId)
        {
            var currentQeustion = await _questionRepository.GetByIdAsync(questionId);
            if (currentQeustion == null)
                throw new ArgumentException($"Question with ID {questionId} not found");
            
            return _mapper.Map<IEnumerable<ChoiceDto>>(currentQeustion.Choices);
        }
    }
} 