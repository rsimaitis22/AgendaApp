using AgendaApp.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.BL.Services
{
    public class UiMessagesService
    {
        Dictionary<string, string> AgendaTranslations { get; }
        string translationLanguage { get; }

        public UiMessagesService(string TranslationLanguage)
        {
            translationLanguage = TranslationLanguage;
        }

        public string ReadDataFromFile()
        {
            try
            {
                //TODO dynamic translations path
                string defaultPath = @"C:\Users\simai\source\repos\Agenda\AgendaApp.BL\Translations\";
                string path = $"{defaultPath}{translationLanguage}.json";
                StringBuilder sb = new StringBuilder();

                using (var reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }
                return sb.ToString();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("FileNot found");
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public TranslationsAgendaObject GetAgendaObjectTranslations()
        {
            return JsonConvert.DeserializeObject<TranslationsAgendaObject>(ReadDataFromFile());
        }
    }
}
