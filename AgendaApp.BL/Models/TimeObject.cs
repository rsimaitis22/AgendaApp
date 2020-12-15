using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Models
{
    public class TimeObject
    {
        public int[] Hours { get; set; }
        public int[] Minutes { get; set; }

        public int selectedStartingMin { get; set; }
        public int selectedStartingHour { get; set; }
        public int selectedFinishMin { get; set; }
        public int selectedFinishHour { get; set; }

        public bool IsStartingDayMinutesSelected { get; set; }
        public bool IsStartingDayHourSelected { get; set; }
        public bool IsStartingDayDaySelected { get; set; }
        public bool IsFinishDayMinutesSelected { get; set; }
        public bool IsFinishDayHourSelected { get; set; }
        public bool IsFinishDayDaySelected { get; set; }

        public DateTime StartingDay { get; set; }
        public DateTime FinishDay { get; set; }
        public List<string> Numbers { get; set; }

        public TimeObject()
        {
            int minutesInHour = 60;
            int hoursInADay = 24;

            Numbers = new List<string>();
            Hours = new int[hoursInADay];
            Minutes = new int[minutesInHour];

            for (int i = 0; i < hoursInADay; i++)
            {
                Hours[i] = i;
                Numbers.Add(i.ToString());
            }
            for (int i = 0; i < minutesInHour; i++)
            {
                Minutes[i] = i;
            }
            
        }
    }
}
