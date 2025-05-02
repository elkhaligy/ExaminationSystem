namespace ExaminationSystem.Core.Entities
{
    public class StudentAnswer
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public int QuestionId { get; set; }
        public virtual Question? Question { get; set; }

        public int ChoiceId { get; set; }
        public virtual Choice? Choice { get; set; }
    }

}
