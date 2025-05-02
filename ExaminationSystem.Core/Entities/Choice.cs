namespace ExaminationSystem.Core.Entities
{
    public class Choice
    {
        public int Id { get; set; }
        public required string Text { get; set; }

        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}
