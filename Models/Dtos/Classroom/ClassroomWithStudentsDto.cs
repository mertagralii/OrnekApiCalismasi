using OrnekApiCalismasi.Models.Dtos.Student;

namespace OrnekApiCalismasi.Models.Dtos.Classroom;

public class ClassroomWithStudentsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<StudentDto> Students { get; set; }
}