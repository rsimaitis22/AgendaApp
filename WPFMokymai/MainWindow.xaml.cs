using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AgendaApp.DL.Models;
using AgendaApp.BL.Services;
using AgendaApp.BL.Models;
using WPFMokymai;
using AgendaApp.BL.Interfaces;

namespace AgendaApp
{
    public partial class MainWindow : Window
    {
        private const string windowName = "MainWindow";
        private const int defaultIndex = 0;

        IUiMessagesService messagesService;
        IAgendaViewerManager agendaViewerManager;

        TranslationsMainWindowObject translationsMainWindowObject;
        List<AgendaItem> agendaItemList;
        WeekObj weekObject;
        List<LanguageItem> languagesList;
        string selectedLanguage;


        public MainWindow()
        {
            agendaItemList = new List<AgendaItem>();
            agendaViewerManager = new AgendaViewerManager();
            weekObject = new WeekObj();

            languagesList = new List<LanguageItem>() {
                new LanguageItem(){Id=1,Name="EN"},
                new LanguageItem(){Id=2,Name="LT"}
            };

            InitializeComponent();

            InitializeLanguage();
            TranslateWindowText();

            agendaItemList = agendaViewerManager.GetMonthlyAgendaItems(DateTime.UtcNow.Month);

            cmbBoxLanguages.SelectedIndex = defaultIndex; 
            cmbBoxLanguages.ItemsSource = languagesList;

            lstBoxSidePanel.ItemsSource = agendaItemList.Where(x=>!x.IsCompleted);
            sidePanelScrollViewer.Content = lstBoxSidePanel;

            UpdateWeeklyAgendaList();
            //TODO perkraut UI po pakeitimu
        }

        private void InitializeLanguage()
        {
            selectedLanguage = "EN";
            messagesService = new UiMessagesService(selectedLanguage);
        }

        private void btnCreateNewAgenda_Click(object sender, RoutedEventArgs e)
        {
            NewAgendaItem item = new NewAgendaItem(selectedLanguage);
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
            selectedLanguage = selectedLang.Name;

            //TODO jei egzistuoja vienos kalbos serviso instance kito tokio pat daugiau nekurti
            messagesService = new UiMessagesService(selectedLanguage);
            TranslateWindowText();
        }

        private void chkBoxShowNearest_Checked(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            if (check.IsChecked==true)
                lstBoxSidePanel.ItemsSource = agendaItemList.Where(x=>x.FinishDate >= DateTime.Now).OrderBy(x => x.FinishDate);
            else
                lstBoxSidePanel.ItemsSource = agendaItemList;
            
        }

        private void chkBoxOrderByPriority_Checked(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            if (check.IsChecked == true)
                lstBoxSidePanel.ItemsSource = agendaItemList.OrderByDescending(x => x.Priority);
            else 
                lstBoxSidePanel.ItemsSource = agendaItemList;
        }

        private void btnNextWeek_Click(object sender, RoutedEventArgs e)
        {
            weekObject.GetNextWeek();

            if (weekObject.SelectedWeek[0].Month != weekObject.SelectedWeek[6].Month)
            {
                agendaItemList.AddRange(agendaViewerManager.GetMonthlyAgendaItems(weekObject.SelectedWeek[6].Month));
            }

            UpdateWeeklyAgendaList();
        }

        private void btnPreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            weekObject.GetPreviousWeek();

            if (weekObject.SelectedWeek[0].Month != weekObject.SelectedWeek[6].Month)
            {
                agendaItemList.AddRange(agendaViewerManager.GetMonthlyAgendaItems(weekObject.SelectedWeek[0].Month));
            }

            UpdateWeeklyAgendaList();
        }
        private void UpdateWeeklyAgendaList()
        {
            //TODO sutvarkyt filtravima
            lblWeekNumber.Content = weekObject.GetCurrentWeekNumber();
            lstBoxMonday.ItemsSource = agendaItemList.Where(x => !x.IsCompleted).Where(x => (x.FinishDate.Day == weekObject.SelectedWeek[(int)DayOfWeekEnum.Monday].Day) && (x.FinishDate.Month==weekObject.SelectedWeek[(int)DayOfWeekEnum.Monday].Month));
            lstBoxTuesday.ItemsSource = agendaItemList.Where(x => !x.IsCompleted).Where(x => (x.FinishDate.Day == weekObject.SelectedWeek[(int)DayOfWeekEnum.Tuesday].Day) && (x.FinishDate.Month == weekObject.SelectedWeek[(int)DayOfWeekEnum.Tuesday].Month));
            lstBoxWednsday.ItemsSource = agendaItemList.Where(x => !x.IsCompleted).Where(x => (x.FinishDate.Day == weekObject.SelectedWeek[(int)DayOfWeekEnum.Wendsday].Day) && (x.FinishDate.Month == weekObject.SelectedWeek[(int)DayOfWeekEnum.Wendsday].Month));
            lstBoxThursday.ItemsSource = agendaItemList.Where(x => !x.IsCompleted).Where(x => (x.FinishDate.Day == weekObject.SelectedWeek[(int)DayOfWeekEnum.Thursday].Day) && (x.FinishDate.Month == weekObject.SelectedWeek[(int)DayOfWeekEnum.Thursday].Month));
            lstBoxFriday.ItemsSource = agendaItemList.Where(x=>!x.IsCompleted).Where(x => (x.FinishDate.Day == weekObject.SelectedWeek[(int)DayOfWeekEnum.Friday].Day) && (x.FinishDate.Month == weekObject.SelectedWeek[(int)DayOfWeekEnum.Friday].Month));
        }

        private void txtListGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var context = (Grid)sender;
            var agendaItem = context.DataContext;
            AgendaItemWindow agendaWindow = new AgendaItemWindow((AgendaItem)agendaItem, selectedLanguage);
            agendaWindow.Show();
        }
    }
} 