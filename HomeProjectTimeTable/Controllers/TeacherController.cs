using HomeProjectTimeTable.Contexts;
using HomeProjectTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeProjectTimeTable.Controllers
{
    public class TeacherController : Controller
    {
        private readonly DataContext _context;
        public TeacherController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //Контроллер, который возвращает список пользователей
        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            IQueryable<Teacher> teachersIQuer = _context.teachers;
            var teachers = await teachersIQuer.ToListAsync();
            return View(teachers);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Teacher teacher = new();
            return View(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            teacher.CreatedAt = DateTime.Now;
            teacher.ModifiedAt = DateTime.Now;
            await _context.teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _context.teachers.FindAsync(id);
            return View(teacher);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Teacher teacher)
        {
            if (teacher is null)
                throw new ArgumentException(nameof(teacher));
            teacher.ModifiedAt = DateTime.Now;
            _context.teachers.Update(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.teachers.FindAsync(id);
            if (teacher is null)
                throw new ArgumentException(nameof(teacher));
            _context.teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
