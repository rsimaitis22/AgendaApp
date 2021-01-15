using AgendaApp;
using AgendaApp.BL.Interfaces;
using AgendaApp.BL.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFMokymai
{
    public partial class StartingPage : Window
    {
        IUiMessagesService messageService;
        List<LanguageItem> languages;
        ObservableCollection<ConnectionItem> connections;

        public StartingPage()
        {
            InitializeComponent();

            languages = new List<LanguageItem>
            {
                new LanguageItem(){Id=1,Name="English"},
                new LanguageItem(){Id=2,Name="Lietuviškai"}
            };
            connections = new ConnectionItemList();
            InitializeConfig();
            
            comBoxLangSelector.ItemsSource = languages;
            comBoxLangSelector.SelectedIndex = 0;

            comBoxConnectionSelector.ItemsSource = connections;
            comBoxConnectionSelector.SelectedIndex = 0;

        }

        private void comBoxLangSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBox = (ComboBox)sender;
            LanguageItem selectedLang = (LanguageItem)selectedBox.SelectedItem;
            ConfigModel.Language = selectedLang.Name;

            txtBlockLangInfo.Text = $"Language:{ConfigModel.Language}";
        }
        private void InitializeConfig()
        {
            string filePath = $"{Directory.GetCurrentDirectory()}/config.txt";

            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);

                ConfigTemp temp = JsonConvert.DeserializeObject<ConfigTemp>(text);

                WriteToConfigModel(temp);
            }
            else
            {
                ConfigTemp firstTimeInit = new ConfigTemp();
                firstTimeInit.ConnectionType = "CSV";
                firstTimeInit.Language = "English";
                firstTimeInit.LanguageShort = "EN";

                WriteToConfigModel(firstTimeInit);  

                string json = JsonConvert.SerializeObject(firstTimeInit);
                File.AppendAllText(filePath, json);
            }
        }
        void WriteToConfigModel(ConfigTemp temp)
        {
            ConfigModel.ConnectionType = temp.ConnectionType;
            ConfigModel.Language = temp.Language;
            ConfigModel.LanguageShort = temp.LanguageShort;
        }

        private void comBoxConnectionSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedConnection = (ComboBox)sender;
            ConnectionItem selected = (ConnectionItem)selectedConnection.SelectedItem;

            ConfigModel.ConnectionType = selected.Name;

            txtBlockConnType.Text =$"Connection: {ConfigModel.ConnectionType}";
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SaveToConfig();

            MainWindow window = new MainWindow();
            window.Show();

            Close();
        }

        private void SaveToConfig()
        {
            string filePath = $"{Directory.GetCurrentDirectory()}/config.txt";

            ConfigTemp tempConfig = new ConfigTemp();
            tempConfig.ConnectionType = ConfigModel.ConnectionType;
            tempConfig.Language = ConfigModel.Language;
            tempConfig.LanguageShort = ConfigModel.LanguageShort;

            string json = JsonConvert.SerializeObject(tempConfig);
            File.WriteAllText(filePath, json);
        }
    }
    class ConfigTemp
    {
       public string ConnectionType { get; set; }
       public string Language { get; set; }
       public string LanguageShort { get; set; }
    }

    public class ConnectionItemList : ObservableCollection<ConnectionItem>
    {
        public ConnectionItemList() : base()
        {
            Add(new ConnectionItem() { Id = 1, Name = "CSV", IconPath = @"C:\Users\riman\source\repos\AgendaAppGitHub\AgendaApp.BL\Icons\csv-file-format.png" });
            Add(new ConnectionItem() { Id = 2, Name = "ENTITY", IconPath = @"C:\Users\riman\source\repos\AgendaAppGitHub\AgendaApp.BL\Icons\database.png" });
        }
    }
}
