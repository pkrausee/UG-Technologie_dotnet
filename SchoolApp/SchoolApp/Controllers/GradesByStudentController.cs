using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;

namespace SchoolApp.Controllers
{
    public class GradesByStudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradesByStudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Student"] = new SelectList(_context.Student, "Id", "Display");
            return View();
        }

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