using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Models
{
    public class TimeObject
    {
        public int[] Hours { get; set; }
        public int[] Minutes { get; set; }
        public int selectedMin { get; set; }
        public int selectedHour { get; set; }

        public DateTime Date { get; set; }

        public TimeObject()
        {
            int minutesInHour = 60;
            int hoursInADay = 24;

            Hours = new int[hoursInADay];
            Minutes = new int[minutesInHour];

            for (int i = 0; i < hoursInADay; i++)
            {
                Hours[i] = i;
            }
            for (int i = 0; i < minutesInHour; i++)
            {
                Minutes[i] = i;
            }

            selectedMin = 0;
            selectedHour = 0;

            Date = new DateTime();
        }
    }
}
