using HomeProjectTimeTable.Contexts;
using HomeProjectTimeTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeProjectTimeTable.Controllers
{
    public class GroupController : Controller
    {
        private readonly DataContext _context;
        public GroupController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //Контроллер, который возвращает список пользователей
        [HttpGet]
        public async Task<IActionResult> Index(int id = 0)
        {
            IQueryable<Group> groupsIQuer = _context.groups;
            var groups = await groupsIQuer.ToListAsync();
            return View(groups);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Group group = new();
            return View(group);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Group group)
        {
            group.CreatedAt = DateTime.Now;
            group.ModifiedAt = DateTime.Now;
            await _context.groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.groups.FindAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Group group)
        {
            if (group is null)
                throw new ArgumentException(nameof(group));
            group.ModifiedAt = DateTime.Now;
            _context.groups.Update(group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.groups.FindAsync(id);
            if (user is null)
                throw new ArgumentException(nameof(user));
            _context.groups.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
