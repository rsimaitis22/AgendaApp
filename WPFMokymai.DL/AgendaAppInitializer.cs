using AgendaApp.DL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace AgendaApp.DL
{
    internal class AgendaAppInitializer : CreateDatabaseIfNotExists<AgendaDbContext>
    {
        protected override void Seed(AgendaDbContext context)
        {
            List<AgendaItem> agendaItems = new List<AgendaItem>()
            {
                new AgendaItem(){
                    Id=1,
                    Title="Sukurt duombaze",
                    Description="Sukurt dummy data duomenu bazei, ir patalpint i ja duomenis",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,14,22,30,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=3
                },
                new AgendaItem(){
                    Id=2,
                    Title="Main Window list boxai",
                    Description="Sukurti sablona ListBox, pagrindiniame lange",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,13,20,0,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=2
                },
                new AgendaItem(){
                    Id=3,
                    Title="Dienu tvarkymas",
                    Description="Sukurti dienu atvaizdavimo manageri, kuris tvarkytu kiek ir kokiu dienu reikia, atvaizduotu reikiama kieki dienu",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,14,10,0,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=3
                },
                new AgendaItem(){
                    Id=4,
                    Title="Artimiausi ivykiai ListBox",
                    Description="Sukurti artimiausiu ivykiu listbox sablona",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,12,22,0,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=1
                },
                new AgendaItem(){
                    Id=5,
                    Title="Agenda manager fix",
                    Description="Agenda manager turetu grazint dienos, savaites, menesio irasus",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,14,21,30,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=3
                },
                new AgendaItem(){
                    Id=6,
                    Title="Kalbos nustatymas isvaizda",
                    Description="Sukurti veikianti kalbos pasirinkimo mygtuka, dropdown, ar kazka panasaus",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,12,20,0,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=1
                },
                new AgendaItem(){
                    Id=7,
                    Title="Failu nuskaitymo manageris",
                    Description="Sukurti failu nuskaitymo manager, kalbu nuskaitymui",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,13,17,0,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=3
                },
                new AgendaItem(){
                    Id=8,
                    Title="Sutvarkyt vertimus",
                    Description="Sutvarkyt failu nuskaityma kad skaitytu is vieno failo skirtingiems issokanciams langams",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,14,17,0,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=2
                },
                new AgendaItem(){
                    Id=9,
                    Title="Sugalvot svarbos atvaizdavima",
                    Description="Sugalvot kaip atvaizduojama iraso svarba",
                    StartDate=DateTime.UtcNow,
                    FinishDate=new DateTime(2020,12,14,10,0,0),
                    IsCompleted=false,
                    IsRepeatable=false,
                    RepeatableInterval=0,
                    Severity=2
                },
            };

            context.AgendaItems.AddRange(agendaItems);
        }
    }

}