using System;
using System.Collections.Generic;
using System.Linq;
using AgendaApp.BL.Interfaces;
using AgendaApp.DL;
using AgendaApp.DL.Models;

namespace AgendaApp.BL.Services
{
    public class AgendaManager : IAgendaManager
    {
        public AgendaManager()
        {
        }
        public virtual List<AgendaItem> GetCurrentMonthAgendas()
        {
            using (var context = new AgendaDbContext())
            {
                return context.AgendaItems.Where(x => x.FinishDate.Month == DateTime.UtcNow.Month).ToList();
            }
        }
        public virtual List<AgendaItem> GetSelectedMonthAgendas(int selectedMonth)
        {
            using (var context = new AgendaDbContext())
            {
                return context.AgendaItems.Where(x => x.FinishDate.Month == selectedMonth).ToList();
            }
        }
        public virtual List<AgendaItem> GetAllAgendas()
        {
            using (var context = new AgendaDbContext())
            {
                return context.AgendaItems.ToList();
            }
        }
        public virtual AgendaItem GetAgenda(int id)
        {
            using (var context = new AgendaDbContext())
            {
                return context.AgendaItems.FirstOrDefault(x => x.Id == id);
            }
        }
        public virtual AgendaItem ModifyAgendaItem(AgendaItem agendaItem)
        {
            using (var context = new AgendaDbContext())
            {
                var item = context.AgendaItems.FirstOrDefault(x => x.Id == agendaItem.Id);

                if (item != null)
                {
                    item.Description = agendaItem.Description;
                    item.Title = agendaItem.Title;
                    item.FinishDate = agendaItem.FinishDate;
                    item.Priority = agendaItem.Priority;
                    item.IsCompleted = agendaItem.IsCompleted;
                }

                context.SaveChanges();

                return item;
            }
        }
        public virtual void CreateAgenda(AgendaItem item)
        {
            using (var context = new AgendaDbContext())
            {
                context.AgendaItems.Add(item);

                context.SaveChanges();
            }
        }
    }
}
