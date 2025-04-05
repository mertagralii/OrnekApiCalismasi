namespace OrnekApiCalismasi;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }

    public ICollection<Classroom> Classrooms { get; set; }
}