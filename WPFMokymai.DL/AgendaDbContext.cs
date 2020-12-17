using System.Data.Entity;
using AgendaApp.DL.Models;

namespace AgendaApp.DL
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext() : base("AgendaApp")
        {
            Database.SetInitializer(new AgendaAppInitializer());
        }
        public DbSet<AgendaItem> AgendaItems { get; set; }
    }
}

