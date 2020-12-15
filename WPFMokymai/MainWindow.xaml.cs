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
        private const int defaultIndex = 0;

        UiMessagesService messagesService;
        AgendaViewerManager agendaViewerManager;
        TranslationsMainWindowObject translationsMainWindowObject { get; set; }

        public List<LanguageItem> LanguagesList { get; set; }
        public string SelectedLanguage { get; set; }
        public List<AgendaItem> AgendaItemList;

        public MainWindow()
        {
            AgendaItemList = new List<AgendaItem>();
            agendaViewerManager = new AgendaViewerManager();
            LanguagesList = new List<LanguageItem>() {
                new LanguageItem(){Id=1,Name="EN"},
                new LanguageItem(){Id=2,Name="LT"}
            };

            InitializeComponent();

            InitializeLanguage();
            TranslateWindowText();
            AgendaItemList = agendaViewerManager.GetMonthlyAgendaItems(DateTime.UtcNow.Month);

            cmbBoxLanguages.SelectedIndex = defaultIndex; 
            cmbBoxLanguages.ItemsSource = LanguagesList;

            lstBoxSidePanel.ItemsSource = AgendaItemList;
            sidePanelScrollViewer.Content = lstBoxSidePanel;

            //lstBoxWeekDays.ItemsSource = timeObj.Numbers;
            //scrViewerHoursList.Content = lstBoxWeekDays;
        }

        private void InitializeLanguage()
        {
            SelectedLanguage = "EN";
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
        public virtual void TranslateWindowText()
        {
            translationsMainWindowObject = (TranslationsMainWindowObject)messagesService.GetTranslationsObject(windowName);

            Title = translationsMainWindowObject.Title;
            btnCreateNewAgenda.Content = translationsMainWindowObject.NewAgendaButtonText;
            chkBoxOrderByPriority.Content = translationsMainWindowObject.Priority;
            chkBoxShowCompleted.Content = translationsMainWindowObject.Done;
            lblLanguage.Content = translationsMainWindowObject.Language;
        }

        private void cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBox = (ComboBox)sender;
            LanguageItem selectedLang = (LanguageItem)selectedBox.SelectedItem;
            SelectedLanguage = selectedLang.Name;
            //TODO jei egzistuoja vienos kalbos serviso instance kito tokio pat daugiau nekurti
            messagesService = new UiMessagesService(SelectedLanguage);
            TranslateWindowText();
        }
    }
}