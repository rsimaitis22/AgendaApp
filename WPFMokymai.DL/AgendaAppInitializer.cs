using AgendaApp.DL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AgendaApp.DL
{
    internal class AgendaAppInitializer : CreateDatabaseIfNotExists<AgendaDbContext>
    {
        protected override void Seed(AgendaDbContext context)
        {
            string title = "dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            List<string> titleList = title.Split(' ').ToList();

            Random r = new Random();
            List<AgendaItem> agendaItems = new List<AgendaItem>();

            for (int i = 0; i < 3000; i++)
            {
                int month = r.Next(1, 13);
                int day = r.Next(1, 15);

                agendaItems.Add(new AgendaItem()
                {
                    Title = titleList[r.Next(0, titleList.Count)],
                    Description = $"{titleList[r.Next(0, titleList.Count)]} {titleList[r.Next(0, titleList.Count)]} {titleList[r.Next(0, titleList.Count)]}",
                    StartDate = new DateTime(2020, month, day),
                    FinishDate = new DateTime(2020, month, day + r.Next(1, 14)),
                    IsCompleted = false,
                    Priority = r.Next(1, 4)
                }); 
            }


            context.AgendaItems.AddRange(agendaItems);
        }
    }

}
