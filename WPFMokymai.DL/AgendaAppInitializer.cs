using System.Data.Entity;

namespace AgendaApp.DL
{
    internal class AgendaAppInitializer : CreateDatabaseIfNotExists<AgendaDbContext>
    {
        
    }

}