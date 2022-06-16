using HomeProjectTimeTable.Models;
using System.Linq;

namespace HomeProjectTimeTable
{
    public class CalendarBuilder
    {
        public Calendar Build(DateTime date, int _idGroup)
        {
            DateTime temp_date = date;
            Day[,] days = new Day[6, 7];
            int offset = (int)date.DayOfWeek;

            if (offset == 0)
                offset = 7;

            offset--;
            date = date.AddDays(offset * -1);
            for (int i = 0; i != 6; i++)
            {
                for (int j = 0; j != 7; j++)
                {
                    days[i, j] = new DayBuilder().Build(date, _idGroup, temp_date);
                    date = date.AddDays(1);
                }
            }
            return new Calendar()
            {
                Date = date,
                Days = days
            };
        }
    }
}
