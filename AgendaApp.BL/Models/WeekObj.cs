using System;

namespace AgendaApp.BL.Models
{
    public class WeekObj
    {
        const int daysInWeek = 7;
        const int daysWithoutMonday = 6;

        DateTime weekStart;
        
        public DateTime[] SelectedWeek { get; set; } 

        public WeekObj()
        {
            SelectedWeek = new DateTime[daysInWeek];
            GetCurrentWeekDates();
        }
        public void GetCurrentWeekDates()
        {
            var daysTillCurrentDay = DateTime.UtcNow.DayOfWeek - DayOfWeek.Monday;
            weekStart = DateTime.Now.AddDays(-daysTillCurrentDay);
            UpdateDays();
        }
        public double GetCurrentWeekNumber()
        {
            return Math.Ceiling((double)(weekStart.DayOfYear + daysWithoutMonday) / daysInWeek);
        }
        public void GetPreviousWeek()
        {
            weekStart = weekStart.AddDays(-daysInWeek);
            UpdateDays();
        }
        public void GetNextWeek()
        {
            weekStart = weekStart.AddDays(daysInWeek);
            UpdateDays();
        }
        private void UpdateDays()
        {
            for (int i = 0; i < daysInWeek; i++)
            {
                SelectedWeek[i] = weekStart.AddDays(i);
            }
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
