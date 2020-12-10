using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AgendaApp.BL;
using AgendaApp.DL.Models;

namespace AgendaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<AgendaItem> AgendaList;
        List<string> LanguagesList;
        public string SelectedLanguage { get; set; }

        public MainWindow()
        {
            LanguagesList = new List<string>() { "EN","LT"};

            AgendaList = new List<AgendaItem>();

            AgendaList.Add(new AgendaItem() { Id = 1, Title = "Pirmas", Description = "Prausiam suni, perkam maike", IsCompleted = false, IsRepeatable = false, RepeatableInterval = 0, StartDate = DateTime.Now, FinishDate = DateTime.UtcNow });
            AgendaList.Add(new AgendaItem() { Id = 2, Title = "Antras", Description = "Ispirkti visa tualetini popieriu", IsCompleted = false, IsRepeatable = false, RepeatableInterval = 0, StartDate = DateTime.Now, FinishDate = DateTime.UtcNow });
            AgendaList.Add(new AgendaItem() { Id = 3, Title = "Trecias", Description = "Nukasti sniega nuo batu", IsCompleted = false, IsRepeatable = false, RepeatableInterval = 0, StartDate = DateTime.Now, FinishDate = DateTime.UtcNow });

            InitializeComponent();
            

            closestToFinishAgendaListBox.ItemsSource = AgendaList;

        }
        private void listBox_SelectedValue(object sender, RoutedEventArgs e)
        {
            var selected = (ListBox)sender;
            var s = selected.SelectedItem;

        }

        private void btnCreateNewAgenda_Click(object sender, RoutedEventArgs e)
        {
            NewAgendaItem item = new NewAgendaItem();
            item.Show();
        }
    }
}