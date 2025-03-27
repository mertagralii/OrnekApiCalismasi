using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrnekApiCalismasi.Data;

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
}