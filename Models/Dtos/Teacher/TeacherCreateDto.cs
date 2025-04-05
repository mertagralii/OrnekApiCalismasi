using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrnekApiCalismasi.Models.Dtos.Teacher;

public class TeacherCreateDto
{
    // TODO: modelstate dönüldüğünde hata mesajının içeriğini buradan verebileceğimiz kod örneği
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
}