using OrnekApiCalismasi.Models.Dtos.Classroom;

namespace OrnekApiCalismasi.Models.Dtos.Teacher;

public class TeacherDetailDto : TeacherDto
{
    public ICollection<ClassroomWithStudentsDto> Classrooms { get; set; }
}