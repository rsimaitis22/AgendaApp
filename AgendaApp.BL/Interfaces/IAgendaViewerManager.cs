using AgendaApp.DL.Models;
using System.Collections.Generic;

namespace AgendaApp.BL.Interfaces
{
    public interface IAgendaViewerManager
    {
        List<AgendaItem> GetMonthlyAgendaItems(int month);
    }
}