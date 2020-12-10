using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

