using System.Text.Json.Nodes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
        private readonly IMapper _mapper;

        public ClassroomsController(AppDbContext context, IMapper mapper) // mapper
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // listeleme kısmında include yapmak yerine
            // tek sınıf istendiğinde bu ekstra bilgileri vermek daha iyi olabilir
            var classrooms = _context.Classrooms
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .ToArray();

            var result = _mapper.Map<ClassroomDetailDto[]>(classrooms);
            
            return Ok(result);
            
            // return Ok(_context.Classrooms
            //     .Include(c => c.Teacher)
            //     .Include(c => c.Students)
            //     .Select(c => new ClassroomDetailDto
            //     {
            //         Id = c.Id,
            //         Name = c.Name,
            //         Created = c.Created,
            //         Teacher = new TeacherDto
            //         {
            //             Id = c.Teacher.Id,
            //             Name = c.Teacher.Name,
            //         },
            //         Students = c.Students.Select(s => new StudentDto{ Id = s.Id, Name = s.Name }).ToArray()
            //     })
            //     .ToArray());
        }

        [HttpPost("{id}/[action]")]
        public IActionResult AssignStudents(int id, [FromBody]int[] studentIds)
        {
            // TODO: validasyon konusu çözülecek.

            if (studentIds.Length == 0)
            {
                return BadRequest();
            }

            var classroom = _context.Classrooms
                    .Include(c => c.Students)
                    .FirstOrDefault(c => c.Id == id);
            
            if (classroom == null)
            {
                return NotFound();
            }

            var students = _context.Students.Where(s => studentIds.Contains(s.Id)).ToArray();

            if (students.Length == 0)
            {
                return BadRequest();
            }
            
            // var student =  _context.Students.Find(students[0]);
           
            foreach (var student in students)
            {
                classroom.Students.Add(student);
            }
            
            _context.SaveChanges();
            
            return Ok();
        }
    }
}
