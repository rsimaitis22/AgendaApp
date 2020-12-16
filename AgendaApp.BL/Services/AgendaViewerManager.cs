using AgendaApp.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Services
{
    public class AgendaViewerManager
    {
        AgendaManager agendaManager;
        Dictionary<int, List<AgendaItem>> agendaDictionary;

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
