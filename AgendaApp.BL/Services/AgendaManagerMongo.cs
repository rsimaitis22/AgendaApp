using AgendaApp.BL.Interfaces;
using AgendaApp.DL.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Services
{
    public class AgendaManagerMongo : IAgendaManager
    {
        private const string connectionString = "mongodb://localhost:27017";

        public void CreateAgenda(AgendaItem item)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Develop");
            var collection = database.GetCollection<BsonDocument>("agendas");

            var tempDoc = new BsonDocument
            {
                {"_id", GetNewestAgendaId() },
                { "Title", item.Title },
                { "Description", item.Description },
                { "StartDate", item.StartDate.ToString() },
                { "FinishDate", item.FinishDate.ToString() },
                { "IsCompleted", item.IsCompleted },
                { "IsRepeatable", item.IsRepeatable },
                { "RepeatableInterval", item.RepeatableInterval },
                { "Priority", item.Priority } 
            };

            collection.InsertOne(tempDoc);
        }

        private int GetNewestAgendaId()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Develop");
            var collection = database.GetCollection<BsonDocument>("agendas");

            var sort = Builders<BsonDocument>.Sort.Descending("_id");
            var s = collection.Find(new BsonDocument()).Sort(sort).FirstOrDefault().ToString();

            AgendaItem i = JsonConvert.DeserializeObject<AgendaItem>(s);
            return i.Id;
        }

        public AgendaItem GetAgenda(int id)
        {
            throw new NotImplementedException();
        }

        public List<AgendaItem> GetAllAgendas()
        {
            throw new NotImplementedException();
        }

        public List<AgendaItem> GetCurrentMonthAgendas()
        {
            throw new NotImplementedException();
        }

        public AgendaItem GetNewlyCreatedAgenda()
        {
            throw new NotImplementedException();
        }

        public List<AgendaItem> GetSelectedMonthAgendas(int selectedMonth)
        {
            throw new NotImplementedException();
        }

        public AgendaItem ModifyAgendaItem(AgendaItem agendaItem)
        {
            throw new NotImplementedException();
        }

        public void CreateMultipleRepeatableAgendas(AgendaItem item)
        {
            throw new NotImplementedException();
        }
    }
}
