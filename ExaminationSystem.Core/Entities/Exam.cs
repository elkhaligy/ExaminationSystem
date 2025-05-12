namespace ExaminationSystem.Core.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; } // in minutes
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }
}