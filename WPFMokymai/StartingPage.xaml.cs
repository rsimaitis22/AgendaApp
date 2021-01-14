using AgendaApp.BL.Interfaces;
using AgendaApp.BL.Models;
using System;
using System.Collections.Generic;
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

        public StartingPage()
        {
            InitializeComponent();

            languages = new List<LanguageItem>
            {
                new LanguageItem(){Id=1,Name="English"},
                new LanguageItem(){Id=2,Name="Lietuviškai"}
            };
            comBoxLangSelector.SelectedIndex = 0;
            comBoxLangSelector.ItemsSource = languages;

            //Check if config file exists
            //If exists take data from it and save to Configuration Model 
            //Static config model ?? Persistent throughout all windows??

            //If config data does not exist create empty file
            //Save connection type
            //Save language type
            //Save preferences to file
        }

        private void comBoxLangSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBox = (ComboBox)sender;
            LanguageItem selectedLang = (LanguageItem)selectedBox.SelectedItem;
            var selectedLanguage = selectedLang.Name;
        }
    }
}
