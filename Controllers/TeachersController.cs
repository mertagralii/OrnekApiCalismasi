using Microsoft.AspNetCore.Mvc;
using OrnekApiCalismasi.Data;
using OrnekApiCalismasi.Models.Dtos.Teacher;

namespace OrnekApiCalismasi.Controllers;

[Route("[controller]")]
public class TeachersController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult ListTeachers()
    {
        var teachers = context.Teachers
                                .Select(t => new TeacherDto { Id = t.Id, Name = t.Name })
                                .ToArray();
        
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public IActionResult GetTeacher(int id)
    {
        var teacher = context.Teachers.Find(id);
        // find doğrudan primary key üzerinde arama yapar
        // FirstOrDefault'tan daha hızlı çalışır
        // bulamazsa null döner
        
        if (teacher == null)
        {
            return NotFound(new { Message = "Teacher not found" });
        }
        
        return Ok(teacher);
        // ileride servislerimizi tüketmeye (consume etmeye) başladığımızda bizim servis cevapları (response) nasıl işlediğimiz çok önemli
        // örneğin nocontent ile sorunsuz baş edebiliyorsak ve rahat yönetiyorsak bu durumda servisimizden ekstra
        // notFound dönmemize gerek olmayabilir
    }

    [HttpPost]
    public IActionResult AddTeacher([FromBody]TeacherCreateDto teacher)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Message = "Eksik veya hatalı giriş yaptınız." });
        }
        
        var newTeacher = new Teacher {
            Name = teacher.Name,
        };
        
        context.Teachers.Add(newTeacher);
        context.SaveChanges();
        
        // hangi id ile eklendiğini belirtmek ve oluşturulma tarihini göstermek istiyorsam newTeacher dönmeliyim
        // eğer sadece işlemin başarılı olduğunu söyleyeceksem. new { success = True } gibi bir sonuç dönebilirim.
        return Ok(newTeacher);
    }
}