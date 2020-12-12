using AgendaApp.BL.Models;
using AgendaApp.DL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public object GetTranslationsObject(string windowName)
        {
            JObject data = JObject.Parse(ReadDataFromFile());
            IList<JToken> results = data[windowName].Children().ToList();

            Dictionary<string, Object> translationObject = new Dictionary<string, Object>();

            translationObject.Add("AgendaWindow", JsonConvert.DeserializeObject<TranslationsAgendaObject>(data[windowName].ToString()));
            translationObject.Add("MainWindow", JsonConvert.DeserializeObject<TranslationsMainWindowObject>(data[windowName].ToString()));
            translationObject.Add("WeeklyReportWindow", JsonConvert.DeserializeObject<TranslationsWeeklyReportWindowObject>(data[windowName].ToString()));
            
            return translationObject[windowName];
        }
    }
}
