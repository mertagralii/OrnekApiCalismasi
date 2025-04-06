using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using OrnekApiCalismasi.Data;
using OrnekApiCalismasi.Models.Dtos.Student;

namespace OrnekApiCalismasi.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok(_context.Students.ToArray());
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _context.Students
            .Include(student => student.Classrooms)
            .FirstOrDefault(s => s.Id == id);
        
        if (student == null)
        {
            return NotFound(new { Message = "Öğrenci bulunamadı" });
        }
        
        return Ok(student);
    }
}