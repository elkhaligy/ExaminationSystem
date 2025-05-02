namespace ExaminationSystem.Core.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
