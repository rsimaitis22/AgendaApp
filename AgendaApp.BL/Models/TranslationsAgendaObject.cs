using System.Collections.Generic;

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
        public string MarkCompleted { get; set; }
        public string EstimatedFinish { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednsday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
    }
}
