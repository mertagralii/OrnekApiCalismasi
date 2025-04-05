using System.Text.Json.Serialization;

namespace OrnekApiCalismasi;

public class Classroom
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    
    public Teacher Teacher { get; set; }
    
    public ICollection<Student> Students { get; set; }
}