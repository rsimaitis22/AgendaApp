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
using AgendaApp.BL.Models;

namespace AgendaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string windowName = "MainWindow";

        List<string> languagesList;
        UiMessagesService messagesService;
        AgendaManager agendaManager;

        TranslationsMainWindowObject TranslationsMainWindowObject { get; set; }
        public string SelectedLanguage { get; set; }

        public MainWindow()
        {
            agendaManager = new AgendaManager();
            languagesList = new List<string>() { "EN","LT"};

            InitializeComponent();

            InitializeLanguage();
            TranslateWindowText();

            languagesListBox.ItemsSource = languagesList;

            sidePanelListBox.ItemsSource = agendaManager.GetAllAgendas();
            sidePanelScrollViewer.Content = sidePanelListBox;

        }

        private void InitializeLanguage()
        {
            SelectedLanguage = "EN";
            messagesService = new UiMessagesService(SelectedLanguage);
        }

        private void languagesListBox_SelectedValue(object sender, RoutedEventArgs e)
        {
            var selected = (ListBox)sender;
            SelectedLanguage = selected.SelectedItem.ToString();

            //TODO jei egzistuoja vienos kalbos serviso instance kito tokio pat daugiau nekurti
            messagesService = new UiMessagesService(SelectedLanguage);
            TranslateWindowText();
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
        public virtual void TranslateWindowText()
        {
            TranslationsMainWindowObject = (TranslationsMainWindowObject)messagesService.GetTranslationsObject(windowName);

            Title = TranslationsMainWindowObject.Title;
            btnCreateNewAgenda.Content = TranslationsMainWindowObject.NewAgendaButtonText;
        }
    }
}