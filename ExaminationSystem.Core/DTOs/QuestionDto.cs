namespace ExaminationSystem.Core.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public int ExamId { get; set; }
        public required string ExamTitle { get; set; }
        public int ChoiceCount { get; set; }
    }

    public class CreateQuestionDto
    {
        public string Text { get; set; } = string.Empty;
        public int ExamId { get; set; }
    }

    public class UpdateQuestionDto
    {
        public required string Text { get; set; }
    }
} 