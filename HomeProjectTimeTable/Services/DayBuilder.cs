using HomeProjectTimeTable.Contexts;
using HomeProjectTimeTable.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;

namespace HomeProjectTimeTable
{
    public class DayBuilder
    {
        private readonly DataContext _context = new DataContext();

        public Day Build(DateTime date, int _idGroup, DateTime temp_date)
        {
            bool check;
            DateTime temp_date1 = temp_date.AddMonths(-1);
            DateTime temp_date2 = temp_date.AddMonths(1);
            IEnumerable<Lesson> _lesson = _context.lessons.Where(w => w.GroupId == _idGroup && w.DayOfWeek == date.DayOfWeek).ToList();
            if (_lesson.Count() != 0) check = true;
            else check = false;
            return new Day()
            {
                Date = date,
                IsNotCurrentMonth = date.Month == temp_date1.Month || date.Month == temp_date2.Month,
                IsWeekendOrHoliday = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday,
                IsToday = date.Date == DateTime.Now.Date,
                IsLesson = check,
                LessonId = _lesson,
            };
        }
    }
}
