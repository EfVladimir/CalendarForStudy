using HomeProjectTimeTable.Contexts;
using HomeProjectTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeProjectTimeTable.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly DataContext _context;
        public ClassroomController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //Контроллер, который возвращает список пользователей
        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            IQueryable<Classroom> classIQuer = _context.classrooms;
            var classs = await classIQuer.ToListAsync();
            return View(classs);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Classroom classroom = new();
            return View(classroom);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Classroom classroom)
        {
            classroom.CreatedAt = DateTime.Now;
            classroom.ModifiedAt = DateTime.Now;
            await _context.classrooms.AddAsync(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var classroom = await _context.groups.FindAsync(id);
            return View(classroom);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Classroom classroom)
        {
            if (classroom is null)
                throw new ArgumentException(nameof(classroom));
            classroom.ModifiedAt = DateTime.Now;
            _context.classrooms.Update(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var classroom = await _context.classrooms.FindAsync(id);
            if (classroom is null)
                throw new ArgumentException(nameof(classroom));
            _context.classrooms.Remove(classroom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
