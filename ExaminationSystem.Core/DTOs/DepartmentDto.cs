namespace ExaminationSystem.Core.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }

    public class CreateDepartmentDto
    {
        public required string Name { get; set; }
    }

    public class UpdateDepartmentDto
    {
        public required string Name { get; set; }
    }
} 