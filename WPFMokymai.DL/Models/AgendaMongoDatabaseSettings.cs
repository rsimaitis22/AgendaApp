using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMokymai.DL.Models
{
    public class AgendaMongoDatabaseSettings : IAgendaMongoDatabaseSettings
    {
        public string AgendaCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IAgendaMongoDatabaseSettings
    {
        string AgendaCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
