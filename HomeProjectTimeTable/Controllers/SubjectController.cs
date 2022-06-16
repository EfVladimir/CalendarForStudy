using HomeProjectTimeTable.Contexts;
using HomeProjectTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeProjectTimeTable.Controllers
{
    public class SubjectController : Controller
    {
        private readonly DataContext _context;
        public SubjectController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //Контроллер, который возвращает список пользователей
        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            IQueryable<Subject> subjectsIQuer = _context.subjects;
            var subjects = await subjectsIQuer.ToListAsync();
            return View(subjects);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Subject subject = new();
            return View(subject);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            subject.CreatedAt = DateTime.Now;
            subject.ModifiedAt = DateTime.Now;
            await _context.subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _context.subjects.FindAsync(id);
            return View(subject);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Subject subject)
        {
            if (subject is null)
                throw new ArgumentException(nameof(subject));
            subject.ModifiedAt = DateTime.Now;
            _context.subjects.Update(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _context.subjects.FindAsync(id);
            if (subject is null)
                throw new ArgumentException(nameof(subject));
            _context.subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
