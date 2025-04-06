using System.ComponentModel.DataAnnotations;

namespace OrnekApiCalismasi.Models.Dtos.Classroom;

public class ClassroomAssignStudentsDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Student { get; set; }
}