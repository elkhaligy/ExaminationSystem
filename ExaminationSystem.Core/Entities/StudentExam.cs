namespace ExaminationSystem.Core.Entities
{
    public class StudentExam
    {
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }

        public int ExamId { get; set; }
        public virtual Exam? Exam { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float Score { get; set; }
    }
}
