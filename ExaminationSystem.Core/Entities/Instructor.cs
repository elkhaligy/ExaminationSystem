namespace ExaminationSystem.Core.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
