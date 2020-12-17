using AgendaApp.BL.Interfaces;
using AgendaApp.DL.Models;
using System.Collections.Generic;

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
        public List<AgendaItem> GetMonthlyAgendaItems(int month)
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
