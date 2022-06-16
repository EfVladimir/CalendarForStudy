using System.ComponentModel.DataAnnotations.Schema;
namespace HomeProjectTimeTable.Models
{
    public class Calendar
    {
        public DateTime Date { get; set; }
        public Day[,]? Days { get; set; }
        public int[] LessonId { get; set; }
    }
}
