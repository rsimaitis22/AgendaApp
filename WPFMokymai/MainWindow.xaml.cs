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
using AgendaApp.BL.Services;

namespace AgendaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<AgendaItem> AgendaList;
        List<string> languagesList;
        UiMessagesService messagesService;
        AgendaManager am = new AgendaManager();

        public string SelectedLanguage { get; set; }

        public MainWindow()
        {
            languagesList = new List<string>() { "EN","LT"};

            InitializeComponent();

            SelectedLanguage = "EN";
            messagesService = new UiMessagesService(SelectedLanguage);

            languagesListBox.ItemsSource = languagesList;

            sidePanelListBox.ItemsSource = am.GetAllAgendas();
            sidePanelScrollViewer.Content = sidePanelListBox;

        }
        private void listBox_SelectedValue(object sender, RoutedEventArgs e)
        {
            var selected = (ListBox)sender;
            SelectedLanguage = selected.SelectedItem.ToString();
            //TODO jei egzistuoja vienos kalbos serviso instance kito tokio pat daugiau nekurti
            messagesService = new UiMessagesService(SelectedLanguage);
        }

        private void btnCreateNewAgenda_Click(object sender, RoutedEventArgs e)
        {
            NewAgendaItem item = new NewAgendaItem(SelectedLanguage);
            item.Show();
        }

        private void sidePanelScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta );
            e.Handled = true;
        }
    }
}