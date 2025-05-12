using AutoMapper;
using ExaminationSystem.Core.DTOs;
using ExaminationSystem.Core.Entities;

namespace ExaminationSystem.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Student mappings
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.DepartmentName, 
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));

            CreateMap<CreateStudentDto, Student>();
            
            CreateMap<UpdateStudentDto, Student>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // StudentExam mappings
            CreateMap<StudentExam, StudentExamDto>()
                .ForMember(dest => dest.ExamTitle,
                    opt => opt.MapFrom(src => src.Exam != null ? src.Exam.Title : string.Empty));
        }
    }
} 