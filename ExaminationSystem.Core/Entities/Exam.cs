namespace ExaminationSystem.Core.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }
}
