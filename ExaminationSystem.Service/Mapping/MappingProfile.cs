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
            // It needs to be like this because Department is 
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.DepartmentName, 
                    opt => opt.MapFrom(src => src.Department!.Name));

            CreateMap<CreateStudentDto, Student>();
            
            CreateMap<UpdateStudentDto, Student>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // StudentExam mappings
            CreateMap<StudentExam, StudentExamDto>()
                .ForMember(dest => dest.ExamTitle,
                    opt => opt.MapFrom(src => src.Exam!.Title));
        }
    }
} 