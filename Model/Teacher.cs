using System.Text.Json.Serialization;

namespace OrnekApiCalismasi;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }

    public ICollection<Classroom> Classes { get; set; }
}