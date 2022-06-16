using HomeProjectTimeTable.Contexts;
using HomeProjectTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeProjectTimeTable.Controllers
{
    public class LessonController : Controller
    {
        private readonly DataContext _context;
        public LessonController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //Контроллер, который возвращает список пользователей
        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            var subjects = await _context.subjects.ToListAsync();
            ViewBag.Subjects = subjects;
            var teachers = await _context.teachers.ToListAsync();
            ViewBag.Teachers = teachers;
            var groups = await _context.groups.ToListAsync();
            ViewBag.Groups = groups;
            var classroom = await _context.classrooms.ToListAsync();
            ViewBag.Classrooms = classroom;
            IQueryable <Lesson> lessonsIQuer = _context.lessons;
            var lessons = await lessonsIQuer.ToListAsync();
            return View(lessons);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var subjects = await _context.subjects.ToListAsync();
            ViewBag.Subjects = subjects;

            var teachers = await _context.teachers.ToListAsync();
            ViewBag.Teachers = teachers;
            
            var groups = await _context.groups.ToListAsync();
            ViewBag.Groups = groups;

            var classroom = await _context.classrooms.ToListAsync();
            ViewBag.Classrooms = classroom;

            var fields = typeof(DayOfWeek).GetFields().Where(fi => fi.IsLiteral);
            string[] fieldNames = fields.Select(fi => fi.Name).ToArray();
            DayOfWeek[] fieldValues = fields.Select(fi => fi.GetRawConstantValue()).Cast<DayOfWeek>().ToArray();
            ViewBag.Days = fieldValues;

            Lesson lesson = new();
            return View(lesson);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Lesson lesson)
        {
            lesson.CreatedAt = DateTime.Now;
            lesson.ModifiedAt = DateTime.Now;
            await _context.lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subjects = await _context.subjects.ToListAsync();
            ViewBag.Subjects = subjects;

            var teachers = await _context.teachers.ToListAsync();
            ViewBag.Teachers = teachers;

            var groups = await _context.groups.ToListAsync();
            ViewBag.Groups = groups;

            var classroom = await _context.classrooms.ToListAsync();
            ViewBag.Classrooms = classroom;

            var fields = typeof(DayOfWeek).GetFields().Where(fi => fi.IsLiteral);
            string[] fieldNames = fields.Select(fi => fi.Name).ToArray();
            DayOfWeek[] fieldValues = fields.Select(fi => fi.GetRawConstantValue()).Cast<DayOfWeek>().ToArray();
            ViewBag.Days = fieldValues;

            var lesson = await _context.lessons.FindAsync(id);
            return View(lesson);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Lesson lesson)
        {
            if (lesson is null)
                throw new ArgumentException(nameof(lesson));
            lesson.ModifiedAt = DateTime.Now;
            _context.lessons.Update(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.lessons.FindAsync(id);
            /*if (user is null)
                throw new ArgumentException(nameof(user));*/
            _context.lessons.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
