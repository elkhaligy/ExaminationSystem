namespace ExaminationSystem.Core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int InstructorId { get; set; }
        public virtual Instructor? Instructor { get; set; }

        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }

}
