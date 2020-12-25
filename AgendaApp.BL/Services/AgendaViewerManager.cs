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
        List<AgendaItem> items;

        public AgendaViewerManager()
        {
            items = new List<AgendaItem>();
            agendaManager = new AgendaManagerEntity();
            agendaDictionary = new Dictionary<int, List<AgendaItem>>();
        }
        public virtual AgendaItem GetNewlyAddedAgenda(int month)
        {
            return agendaManager.GetNewlyCreatedAgenda();
        }
        public virtual List<AgendaItem> GetFutureNearestMonthAgendaItems(int month)
        {
            items = TryGetMonthAgendas(month);

            return items.Where(x=>!x.IsCompleted).Where(x => x.FinishDate >= DateTime.Now).OrderBy(x => x.FinishDate).ToList();
        }
        public virtual List<AgendaItem> GetMonthAgendaItemsByPriority(int month)
        {
            items = TryGetMonthAgendas(month);

            return items.Where(x=>!x.IsCompleted).OrderByDescending(x => x.Priority).ToList();
        }

        public virtual List<AgendaItem> GetMonthlyAgendaItems(int month)
        {
            items = TryGetMonthAgendas(month);

            return items.Where(x => !x.IsCompleted).ToList() ;
        }
        public virtual List<AgendaItem> GetCurrentWeekDayAgendaItems(int month,int day)
        {
            items = TryGetMonthAgendas(month);

            return items.Where(x => !x.IsCompleted).Where(X => X.FinishDate.Day == day).ToList();
        }
        public virtual List<AgendaItem> GetNotCompletedAgendaItems(int month)
        {
            items = TryGetMonthAgendas(month);

            return items.Where(x => !x.IsCompleted).ToList();
        }
        private List<AgendaItem> TryGetMonthAgendas(int month)
        {
            CheckIfAgendaIsInDictionary(month);

            agendaDictionary.TryGetValue(month, out items);
            return items;
        }

        private void CheckIfAgendaIsInDictionary(int month)
        {
            if (!agendaDictionary.ContainsKey(month))
                agendaDictionary.Add(month, agendaManager.GetSelectedMonthAgendas(month));
        }
        public virtual void AddNewlyCreatedAgenda(int month)
        {
            CheckIfAgendaIsInDictionary(month);

            agendaDictionary.FirstOrDefault(x => x.Key == month).Value.Add(agendaManager.GetNewlyCreatedAgenda());
        }
    }
}
