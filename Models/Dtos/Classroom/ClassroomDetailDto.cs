using OrnekApiCalismasi.Models.Dtos.Student;
using OrnekApiCalismasi.Models.Dtos.Teacher;

namespace OrnekApiCalismasi.Models.Dtos.Classroom;

public class ClassroomDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public TeacherDto Teacher { get; set; }
    public ICollection<StudentDto> Students { get; set; }
    
}