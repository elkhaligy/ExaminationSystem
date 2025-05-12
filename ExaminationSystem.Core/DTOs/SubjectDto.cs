namespace ExaminationSystem.Core.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int InstructorId { get; set; }
        public required string InstructorName { get; set; }
    }

    public class CreateSubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public int InstructorId { get; set; }
    }

    public class UpdateSubjectDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int InstructorId { get; set; }
    }
} 