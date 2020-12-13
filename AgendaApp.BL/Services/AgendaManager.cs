using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaApp.DL;
using AgendaApp.DL.Models;

namespace AgendaApp.BL.Services
{
    public class AgendaManager
    {
        public List<AgendaItem> agendaItems { get; set; }

        public AgendaManager()
        {
            agendaItems = new List<AgendaItem>();
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
        public virtual AgendaItem ModifyAgendaDate(int id, DateTime tempDate)
        {
            using (var context = new AgendaDbContext())
            {
                var item = context.AgendaItems.FirstOrDefault(x => x.Id == id);
                item.FinishDate = tempDate;

                context.SaveChanges();

                return item;
            }
        }
        public virtual AgendaItem ModifyAgendaDescription(int id, string description)
        {
            using (var context = new AgendaDbContext())
            {
                var item = context.AgendaItems.FirstOrDefault(x => x.Id == id);
                item.Description = description;

                context.SaveChanges();

                return item;
            }
        }
        public virtual AgendaItem ModifyAgendaTitle(int id, string agendaTitle)
        {
            using (var context = new AgendaDbContext())
            {
                var item = context.AgendaItems.FirstOrDefault(x => x.Id == id);
                item.Title = agendaTitle;

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
