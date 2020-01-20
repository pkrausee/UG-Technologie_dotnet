using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;

namespace SchoolApp.Controllers
{
    [Authorize(Roles = "Administrator, Teacher")]
    public class GradesByStudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradesByStudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Student/Grades")]
        public IActionResult Index()
        {
            ViewData["Student"] = new SelectList(_context.Student, "Id", "Display");
            return View();
        }

        [Route("Student/Grades/Show")]
        public IActionResult Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grades = _context.Grade
                .Where(g => g.StudentId == id)
                .Include(g => g.Subject);

            var student = _context.Student
                .Where(a => a.Id == id)
                .Select(p => p.Display)
                .FirstOrDefault();

            ViewBag.Student = student;

            return View(grades);
        }
    }
}