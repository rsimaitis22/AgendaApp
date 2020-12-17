using AgendaApp.BL.Interfaces;
using AgendaApp.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaApp.BL.Services
{
    public class AgendaViewerManager : IAgendaViewerManager
    {
        IAgendaManager agendaManager { get; }
        Dictionary<int, List<AgendaItem>> agendaDictionary { get; }

        public AgendaViewerManager()
        {
            agendaManager = new AgendaManager();
            agendaDictionary = new Dictionary<int, List<AgendaItem>>();
        }
        public virtual List<AgendaItem> GetFutureNearestMonthAgendaItems(int month)
        {
            List<AgendaItem> items = TryGetMonthAgendas(month);

            return items.Where(x=>!x.IsCompleted).Where(x => x.FinishDate >= DateTime.Now).OrderBy(x => x.FinishDate).ToList();
        }
        public virtual List<AgendaItem> GetMonthAgendaItemsByPriority(int month)
        {
            List<AgendaItem> items = TryGetMonthAgendas(month);

            return items.Where(x=>!x.IsCompleted).OrderByDescending(x => x.Priority).ToList();
        }

        public virtual List<AgendaItem> GetMonthlyAgendaItems(int month)
        {
            List<AgendaItem> items = TryGetMonthAgendas(month);

            return items.Where(x => !x.IsCompleted).ToList() ;
        }
        public virtual List<AgendaItem> GetCurrentWeekDayAgendaItems(int month,int day)
        {
            List<AgendaItem> items = TryGetMonthAgendas(month);

            return items.Where(x => !x.IsCompleted).Where(X => X.FinishDate.Day == day).ToList();
        }
        public virtual List<AgendaItem> GetNotCompletedAgendaItems(int month)
        {
            List<AgendaItem> items = TryGetMonthAgendas(month);

            return items.Where(x => !x.IsCompleted).ToList();
        }
        private List<AgendaItem> TryGetMonthAgendas(int month)
        {
            List<AgendaItem> items = new List<AgendaItem>();
            CheckIfAgendaIsInDictionary(month);

            agendaDictionary.TryGetValue(month, out items);
            return items;
        }

        private void CheckIfAgendaIsInDictionary(int month)
        {
            if (!agendaDictionary.ContainsKey(month))
                agendaDictionary.Add(month, agendaManager.GetSelectedMonthAgendas(month));
        }
    }
}
