namespace ExaminationSystem.Core.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public required string Text { get; set; }

        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }

        public int ChoiceCount => Choices.Count;
        public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
    }

}
