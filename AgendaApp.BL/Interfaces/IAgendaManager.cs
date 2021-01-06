using AgendaApp.DL.Models;
using System.Collections.Generic;

namespace AgendaApp.BL.Interfaces
{
    public interface IAgendaManager
    {
        void CreateAgenda(AgendaItem item);
        void CreateMultipleRepeatableAgendas(AgendaItem item);
        AgendaItem GetAgenda(int id);
        List<AgendaItem> GetAllAgendas();
        List<AgendaItem> GetCurrentMonthAgendas();
        AgendaItem GetNewlyCreatedAgenda();
        List<AgendaItem> GetSelectedMonthAgendas(int selectedMonth);
        AgendaItem ModifyAgendaItem(AgendaItem agendaItem);
    }
}