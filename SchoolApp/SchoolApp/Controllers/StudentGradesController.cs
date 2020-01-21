namespace SchoolApp.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Data;

    [Authorize]
    public class StudentGradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentGradesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Student/Grades")]
        public IActionResult Index()
        {
            ViewData["Student"] = new SelectList(_context.Student, "Id", "Display");
            return View();
        }

        [Route("Student/Grade/Show")]
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
                .FirstOrDefault(a => a.Id == id);

            ViewBag.Student = student;

            return View(grades);
        }
    }
}