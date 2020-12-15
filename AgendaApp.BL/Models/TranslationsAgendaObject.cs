using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Models
{
    public class TranslationsAgendaObject
    {
        public string WindowTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }
        public string Days { get; set; }
        public string DaySelector { get; set; }
        public string ItemSaveButtonText { get; set; }
        public string ItemExitButtonText { get; set; }
        public string Priority { get; set; }
        public List<PriorityItem> PriorityList { get; set; }
        public string StartDay { get; set; }
        public string FinishDay { get; set; }
    }
}
