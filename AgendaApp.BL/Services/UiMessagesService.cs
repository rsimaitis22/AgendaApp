using AgendaApp.BL.Interfaces;
using AgendaApp.BL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AgendaApp.BL.Services
{
    public class UiMessagesService : IUiMessagesService
    {
        Dictionary<string, Object> translationObject { get; }

        public string TranslationLanguage { get; }

        public UiMessagesService(string translationLanguage)
        {
            TranslationLanguage = translationLanguage;
            translationObject = new Dictionary<string, Object>();
            InitializeTranslationObjects();
        }
        public void SelectLanguage(string translationLanguage) 
        { 

        }

        public string ReadDataFromFile()
        {
            try
            {
                //TODO dynamic translations path

                string p = Directory.GetCurrentDirectory();

                string defaultPath = @"C:\Users\simai\source\repos\Agenda\AgendaApp.BL\Translations\";
                string path = $"{defaultPath}{TranslationLanguage}.json";
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
                Console.WriteLine("Translation file not found");
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void InitializeTranslationObjects()
        {
            JObject data = JObject.Parse(ReadDataFromFile());
            
            try
            {
                translationObject.Add(WindowNamesEnum.AgendaWindow.ToString(), JsonConvert.DeserializeObject<TranslationsAgendaObject>(data[WindowNamesEnum.AgendaWindow.ToString()].ToString()));
                translationObject.Add(WindowNamesEnum.MainWindow.ToString(), JsonConvert.DeserializeObject<TranslationsMainWindowObject>(data[WindowNamesEnum.MainWindow.ToString()].ToString()));
            }
            catch (Exception)
            {
                //log data
                throw;
            }
        }
        public object GetTranslationsObject(string windowName)
        {
            return translationObject[windowName];
        }
    }
}
