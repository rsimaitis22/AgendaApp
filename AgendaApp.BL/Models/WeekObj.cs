using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Models
{
    public class WeekObj
    {
        const int daysInWeek = 7;
        const int daysWithoutMonday = 6;

        DateTime weekStart;
        
        public DateTime[] SelectedWeek = new DateTime[daysInWeek];

        public WeekObj()
        {
            GetCurrentWeekDates();
        }
        public void GetCurrentWeekDates()
        {
            var daysTillCurrentDay = DateTime.UtcNow.DayOfWeek - DayOfWeek.Monday;
            weekStart = DateTime.Now.AddDays(-daysTillCurrentDay);

            for (int i = 0; i < daysInWeek; i++)
            {
                SelectedWeek[i] = weekStart.AddDays(i);
            }
        }
        public double GetCurrentWeek()
        {
            var daysTillCurrentDay = DateTime.UtcNow.DayOfWeek - DayOfWeek.Monday;
            DateTime weekStart = DateTime.Now.AddDays(-daysTillCurrentDay);

            return Math.Ceiling((double)(weekStart.DayOfYear + daysWithoutMonday) / daysInWeek);
        }
    }
    public enum DayOfWeekEnum
    {
        Monday = 0,
        Tuesday = 1,
        Wendsday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday =5,
        Sunday = 6
    }
}
