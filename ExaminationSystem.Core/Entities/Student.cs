namespace ExaminationSystem.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }
}
