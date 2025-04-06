using System.Text.Json.Serialization;

namespace OrnekApiCalismasi;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public ICollection<Classroom> Classrooms { get; set; }
}