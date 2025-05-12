namespace ExaminationSystem.Core.DTOs
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DepartmentId { get; set; }
        public required string DepartmentName { get; set; }
    }

    public class CreateInstructorDto
    {
        public string Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }

    public class UpdateInstructorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DepartmentId { get; set; }
    }
} 