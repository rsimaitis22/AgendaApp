using AgendaApp.DL.Models;
using System.Collections.Generic;

namespace AgendaApp.BL.Interfaces
{
    public interface IAgendaViewerManager
    {
        List<AgendaItem> GetCurrentWeekDayAgendaItems(int month, int day);
        List<AgendaItem> GetFutureNearestMonthAgendaItems(int month);
        List<AgendaItem> GetMonthAgendaItemsByPriority(int month);
        List<AgendaItem> GetMonthlyAgendaItems(int month);
        List<AgendaItem> GetNotCompletedAgendaItems(int month);
    }
}