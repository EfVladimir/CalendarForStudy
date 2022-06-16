using HomeProjectTimeTable.Contexts;
using HomeProjectTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeProjectTimeTable.Controllers
{
    public class CalendarController : Controller
    {
        private readonly DataContext _context;
        public CalendarController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        [HttpGet]
        public async Task<IActionResult> Index(string data = "Июнь 2022", int id = 1, int _ind = 0)
        {
            Calendar cal = new Calendar();
            int[] days = new int[7] { 1, 2, 3, 4, 5, 6, 0 };
            ViewBag.Days = days;
            int[] ind = { -1, 1 };
            ViewBag.Ind = ind;
            var groups = await _context.groups.ToListAsync();
            ViewBag.Groups = groups;
            DateTime date = DateTime.Parse(data);
            DateTime _date;
            if (_ind == 0)
            {
                _date = date;
                ViewBag.Data = _date.ToString("MMMM yyyy");
            }
            else
            {
                _date = date.AddMonths(_ind);
                ViewBag.Data = _date.ToString("MMMM yyyy");
            }
            cal = new CalendarBuilder().Build(_date, id);
            var DisplayGroup = await _context.groups.FindAsync(id);
            ViewBag.DisplayGroup = DisplayGroup.Name;
            ViewBag.Group = id;
            return View(cal);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int idDay, int idGroup)
        {
            var subjects = await _context.subjects.ToListAsync();
            ViewBag.Subjects = subjects;
            var teachers = await _context.teachers.ToListAsync();
            ViewBag.Teachers = teachers;
            var groups = await _context.groups.ToListAsync();
            ViewBag.Groups = groups;
            var classroom = await _context.classrooms.ToListAsync();
            ViewBag.Classrooms = classroom;

            var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Cast<int>().ToList();

            var fields = typeof(DayOfWeek).GetFields().Where(fi => fi.IsLiteral);
            string[] fieldNames = fields.Select(fi => fi.Name).ToArray();
            DayOfWeek[] fieldValues = fields.Select(fi => fi.GetRawConstantValue()).Cast<DayOfWeek>().ToArray();

            IQueryable<Lesson> lessonsIQuer = _context.lessons;
            var lessons = await lessonsIQuer.Where(w=>w.DayOfWeek == fieldValues[idDay] && w.GroupId == idGroup).ToListAsync();

            if (lessons != null)
                return PartialView("Detail", lessons);
            else
            return View("~/Views/Home/Privacy.cshtml");
        }
    }
}
