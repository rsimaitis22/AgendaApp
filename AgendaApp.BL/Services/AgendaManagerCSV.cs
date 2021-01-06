using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaApp.BL.Interfaces;
using AgendaApp.DL.Models;

namespace AgendaApp.BL.Services
{
    public class AgendaManagerCSV : IAgendaManager
    {
        string path = Directory.GetCurrentDirectory() + "/data.txt";

        public void CreateAgenda(AgendaItem item)
        {
            int tempId= 0;
            var tempAgendaItem = GetNewlyCreatedAgenda();
            if (tempAgendaItem != null)
                tempId = tempAgendaItem.Id + 1;
            else
                tempId = 1;

            using (StreamWriter sw = File.AppendText(path))
            {
                string tempString = $"{tempId}|{item.Title}|{item.Description}|{item.StartDate}|{item.FinishDate}|{item.IsCompleted}|{item.IsRepeatable}|{item.RepeatableInterval}|{item.Priority}";
                sw.WriteLine(tempString);
            }
        }

        public void CreateMultipleRepeatableAgendas(AgendaItem item)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                string tempString = $"{item.Id}|{item.Title}|{item.Description}|{item.StartDate}|{item.FinishDate}|{item.IsCompleted}|{item.IsRepeatable}|{item.RepeatableInterval}|{item.Priority}";
                sw.WriteLine(tempString);
            }
        }

        public AgendaItem GetAgenda(int id)
        {
            List<AgendaItem> agendaItems = GetAllAgendas();
            return agendaItems.FirstOrDefault(x => x.Id == id);
        }

        public List<AgendaItem> GetAllAgendas()
        {
            List<AgendaItem> agendaItems = new List<AgendaItem>();
            string singleLine;

            using (StreamReader sr = new StreamReader(path))
            {
                while((singleLine = sr.ReadLine()) != null)
                {
                    agendaItems.Add(ReturnAgendaItemFromLine(singleLine));
                }    
            }
            return agendaItems;
        }

        public List<AgendaItem> GetCurrentMonthAgendas()
        {
            List<AgendaItem> agendaItems = GetAllAgendas();
            return agendaItems.Where(x=>x.FinishDate.Month == DateTime.UtcNow.Month).ToList();
        }

        public AgendaItem GetNewlyCreatedAgenda()
        {
            List<AgendaItem> agendaItems = GetAllAgendas();
            return agendaItems.Last();
        }

        public List<AgendaItem> GetSelectedMonthAgendas(int selectedMonth)
        {
            List<AgendaItem> agendaItems = GetAllAgendas();
            return agendaItems.Where(x => x.FinishDate.Month == selectedMonth).ToList();
        }

        public AgendaItem ModifyAgendaItem(AgendaItem agendaItem)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string temp;
            AgendaItem item = new AgendaItem();
            using (var sr = new StreamReader(path))
            {
                while((temp = sr.ReadLine()) != null)
                {
                    if(ReturnAgendaItemFromLine(temp).Id == agendaItem.Id)
                    {
                        item.Description = agendaItem.Description;
                        item.Title = agendaItem.Title;
                        item.FinishDate = agendaItem.FinishDate;
                        item.Priority = agendaItem.Priority;
                        item.IsCompleted = agendaItem.IsCompleted;

                        string tempString = $"{agendaItem.Id}|{agendaItem.Title}|{agendaItem.Description}|{agendaItem.StartDate}|{agendaItem.FinishDate}|{agendaItem.IsCompleted}|{agendaItem.IsRepeatable}|{agendaItem.RepeatableInterval}|{agendaItem.Priority}";
                        stringBuilder.AppendLine(tempString);
                    }
                    else
                        stringBuilder.AppendLine(temp);
                }
            }

            using (var sw = new StreamWriter(path))
            {
                sw.Write(stringBuilder);
            }

            return item;
        }

        private AgendaItem ReturnAgendaItemFromLine(string v)
        {
            var split = v.Split('|');

            return new AgendaItem()
            {
                Id = Convert.ToInt32(split[0]),
                Title = split[1],
                Description = split[2],
                StartDate = DateTime.Parse(split[3]),
                FinishDate = DateTime.Parse(split[4]),
                IsCompleted = Convert.ToBoolean(split[5]),
                IsRepeatable = Convert.ToBoolean(split[6]),
                RepeatableInterval = Convert.ToInt32(split[7]),
                Priority = Convert.ToInt32(split[8])
            };
        }
    }
}
