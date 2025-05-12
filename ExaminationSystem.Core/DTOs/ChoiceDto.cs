namespace ExaminationSystem.Core.DTOs
{
    public class ChoiceDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class CreateChoiceDto
    {
        public string Text { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class UpdateChoiceDto
    {
        public required string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
} 