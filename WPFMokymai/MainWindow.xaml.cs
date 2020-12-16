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
        public WeekObj WeekObject;


        public MainWindow()
        {
            AgendaItemList = new List<AgendaItem>();
            agendaViewerManager = new AgendaViewerManager();
            WeekObject = new WeekObj();

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

            UpdateWeeklyAgendaList();
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
            chkBoxShowNearest.Content = translationsMainWindowObject.Nearest;
            lblLanguage.Content = translationsMainWindowObject.Language;
            lblMonday.Content = translationsMainWindowObject.Monday;
            lblTuesday.Content = translationsMainWindowObject.Tuesday;
            lblWednsday.Content = translationsMainWindowObject.Wednsday;
            lblThursday.Content = translationsMainWindowObject.Thursday;
            lblFriday.Content = translationsMainWindowObject.Friday;
            lblWeekName.Content = translationsMainWindowObject.Week;
            btnNextWeek.Content = translationsMainWindowObject.Next;
            btnPreviousWeek.Content = translationsMainWindowObject.Previous;
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

        private void chkBoxShowNearest_Checked(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            if (check.IsChecked==true)
                lstBoxSidePanel.ItemsSource = AgendaItemList.Where(x=>x.FinishDate > DateTime.Now).OrderBy(x => x.FinishDate);
            else
                lstBoxSidePanel.ItemsSource = AgendaItemList;
            
        }

        private void chkBoxOrderByPriority_Checked(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            if (check.IsChecked == true)
                lstBoxSidePanel.ItemsSource = AgendaItemList.OrderByDescending(x => x.Priority);
            else 
                lstBoxSidePanel.ItemsSource = AgendaItemList;
        }

        private void btnNextWeek_Click(object sender, RoutedEventArgs e)
        {
            WeekObject.GetNextWeek();

            if (WeekObject.SelectedWeek[0].Month != WeekObject.SelectedWeek[6].Month)
            {
                AgendaItemList.AddRange(agendaViewerManager.GetMonthlyAgendaItems(WeekObject.SelectedWeek[6].Month));
            }

            UpdateWeeklyAgendaList();
        }

        private void btnPreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            WeekObject.GetPreviousWeek();

            if (WeekObject.SelectedWeek[0].Month != WeekObject.SelectedWeek[6].Month)
            {
                AgendaItemList.AddRange(agendaViewerManager.GetMonthlyAgendaItems(WeekObject.SelectedWeek[0].Month));
            }

            UpdateWeeklyAgendaList();
        }
        private void UpdateWeeklyAgendaList()
        {
            lblWeekNumber.Content = WeekObject.GetCurrentWeekNumber();
            lstBoxMonday.ItemsSource = AgendaItemList.Where(x => (x.FinishDate.Day == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Monday].Day) && (x.FinishDate.Month==WeekObject.SelectedWeek[(int)DayOfWeekEnum.Monday].Month));
            lstBoxTuesday.ItemsSource = AgendaItemList.Where(x => (x.FinishDate.Day == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Tuesday].Day) && (x.FinishDate.Month == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Tuesday].Month));
            lstBoxWednsday.ItemsSource = AgendaItemList.Where(x => (x.FinishDate.Day == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Wendsday].Day) && (x.FinishDate.Month == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Wendsday].Month));
            lstBoxThursday.ItemsSource = AgendaItemList.Where(x => (x.FinishDate.Day == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Thursday].Day) && (x.FinishDate.Month == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Thursday].Month));
            lstBoxFriday.ItemsSource = AgendaItemList.Where(x => (x.FinishDate.Day == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Friday].Day) && (x.FinishDate.Month == WeekObject.SelectedWeek[(int)DayOfWeekEnum.Friday].Month));
        }
    }
} 