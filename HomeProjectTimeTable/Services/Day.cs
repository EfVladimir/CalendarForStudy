namespace HomeProjectTimeTable.Models
{
    public class Day
    {
        public DateTime Date { get; set; }
        public bool IsNotCurrentMonth { get; set; }
        public bool IsWeekendOrHoliday { get; set; }
        public bool IsToday { get; set; }
        public bool IsLesson { get;set; }
        public IEnumerable<Lesson> LessonId { get; set; }
    }
}
