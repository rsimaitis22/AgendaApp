using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Models
{
    public class TranslationsAgendaObject
    {
        public string AgendaWindowTitle { get; set; }
        public string AgendaTitle { get; set; }
        public string AgendaDescription { get; set; }
        public string AgendaHours { get; set; }
        public string AgendaMinutes { get; set; }
        public string AgendaDays { get; set; }
        public string AgendaDaySelector { get; set; }
        public string AgendaItemSaveButtonText { get; set; }
        public string AgendaItemExitButtonText { get; set; }
    }
}
