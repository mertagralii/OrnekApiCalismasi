using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrnekApiCalismasi.Data;
using OrnekApiCalismasi.Models.Dtos.Classroom;
using OrnekApiCalismasi.Models.Dtos.Student;
using OrnekApiCalismasi.Models.Dtos.Teacher;

namespace OrnekApiCalismasi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassroomsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.Classrooms
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .Select(c => new ClassroomDetailDto
                {
                    Id = c.Id, 
                    Name = c.Name, 
                    Created = c.Created, 
                    Teacher = new TeacherDto
                    {
                        Id = c.Teacher.Id,
                        Name = c.Teacher.Name,
                    },
                    Students = c.Students.Select(s => new StudentDto{ Id = s.Id, Name = s.Name }).ToArray()
                })
                .ToArray());
        }
    }
}
