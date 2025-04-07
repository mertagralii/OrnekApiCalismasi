using AutoMapper;
using OrnekApiCalismasi.Models.Dtos.Classroom;
using OrnekApiCalismasi.Models.Dtos.Student;
using OrnekApiCalismasi.Models.Dtos.Teacher;

namespace OrnekApiCalismasi;

public class MappingProfile : Profile // AutoMapper NugetPack'i indirdikten sonra bu işlemi yapmamız gerekiyor.
{
    public MappingProfile()
    {
        CreateMap<Teacher, TeacherDto>();
        CreateMap<Teacher, TeacherDetailDto>();
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<Student, StudentDto>();
        CreateMap<Classroom, ClassroomDetailDto>();
        CreateMap<Classroom, ClassroomWithStudentsDto>();
    }
}