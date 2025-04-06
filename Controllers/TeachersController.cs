using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrnekApiCalismasi.Data;
using OrnekApiCalismasi.Models.Dtos.Teacher;

namespace OrnekApiCalismasi.Controllers;

[Route("[controller]")]
public class TeachersController(AppDbContext context, IMapper mapper) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public IActionResult ListTeachers()
    {
        var teachers = _mapper.Map<TeacherDto[]>(_context.Teachers.ToArray());
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public IActionResult GetTeacher(int id)
    {
        var teacher = _context.Teachers.Find(id);
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
        
        // var newTeacher = new Teacher {
        //     Name = teacher.Name,
        // };
        var newTeacher = _mapper.Map<Teacher>(teacher);
        
        _context.Teachers.Add(newTeacher);
        _context.SaveChanges();
        
        var result = _mapper.Map<TeacherDto>(newTeacher);
        
        // hangi id ile eklendiğini belirtmek ve oluşturulma tarihini göstermek istiyorsam newTeacher dönmeliyim
        // eğer sadece işlemin başarılı olduğunu söyleyeceksem. new { success = True } gibi bir sonuç dönebilirim.
        // return Ok(result);
        
        return CreatedAtAction(nameof(GetTeacher), new { id = result.Id }, result);
    }

    [HttpGet("{id}/details")]
    public IActionResult TeacherDetails(int id)
    {
        var teacher = _context.Teachers
            .Include(t => t.Classrooms)
                .ThenInclude(c => c.Students)
            .FirstOrDefault(t => t.Id == id);
        
        var result = _mapper.Map<TeacherDetailDto>(teacher);
        
        return Ok(result);
    }
}