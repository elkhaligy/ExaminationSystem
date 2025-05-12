namespace ExaminationSystem.Core.DTOs
{
    public class ExamDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; } // in minutes
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
    }

    public class CreateExamDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
    }

    public class UpdateExamDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
    }
} 