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
        WeekObj weekObject;
        List<LanguageItem> languagesList;
        string selectedLanguage;

        public MainWindow()
        {
            agendaViewerManager = new AgendaViewerManager();
            weekObject = new WeekObj();

            languagesList = new List<LanguageItem>() {
                new LanguageItem(){Id=1,Name="EN"},
                new LanguageItem(){Id=2,Name="LT"}
            };

            InitializeComponent();

            InitializeLanguage();
            TranslateWindowText();

            cmbBoxLanguages.SelectedIndex = defaultIndex; 
            cmbBoxLanguages.ItemsSource = languagesList;

            lstBoxSidePanel.ItemsSource = agendaViewerManager.GetNotCompletedAgendaItems(DateTime.UtcNow.Month);
            sidePanelScrollViewer.Content = lstBoxSidePanel;

            UpdateWeeklyAgendaList();
        }

        private void InitializeLanguage()
        {
            selectedLanguage = "EN";
            messagesService = new UiMessagesService(selectedLanguage);
        }

        private void btnCreateNewAgenda_Click(object sender, RoutedEventArgs e)
        {
            NewAgendaItem item = new NewAgendaItem(selectedLanguage);
            item.DataChanged += NewAgendaItem_DataChanged;

            item.Show();
        }

        private void NewAgendaItem_DataChanged(object sender, EventArgs e)
        {
            var item = (List<AgendaItem>)sender;
            var eventArgs = e;

            if(item.Count > 1)
            {
                agendaViewerManager.AddMultipleNewlyCreatedAgendas(DateTime.Now.Month, item);
                //agendaViewerManager.AddNewlyCreatedAgenda(date.Month);
          
            }
            else
            {
                agendaViewerManager.AddNewlyCreatedAgenda(item.FirstOrDefault().FinishDate.Month);
            }
            
            UpdateWeeklyAgendaList();
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

            if (check.IsChecked == true)
                lstBoxSidePanel.ItemsSource = agendaViewerManager.GetFutureNearestMonthAgendaItems(DateTime.UtcNow.Month);
            else
                lstBoxSidePanel.ItemsSource = agendaViewerManager.GetNotCompletedAgendaItems(DateTime.UtcNow.Month);
            
        }

        private void chkBoxOrderByPriority_Checked(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            if (check.IsChecked == true)
                lstBoxSidePanel.ItemsSource = agendaViewerManager.GetMonthAgendaItemsByPriority(DateTime.UtcNow.Month);
            else 
                lstBoxSidePanel.ItemsSource = agendaViewerManager.GetNotCompletedAgendaItems(DateTime.UtcNow.Month);
        }

        private void btnNextWeek_Click(object sender, RoutedEventArgs e)
        {
            weekObject.GetNextWeek();

            UpdateWeeklyAgendaList();
        }

        private void btnPreviousWeek_Click(object sender, RoutedEventArgs e)
        {
            weekObject.GetPreviousWeek();

            UpdateWeeklyAgendaList();
        }
        private void UpdateWeeklyAgendaList()
        {
            //TODO sutvarkyt filtravima
            lblWeekNumber.Content = weekObject.GetCurrentWeekNumber();

            lstBoxMonday.ItemsSource = agendaViewerManager.GetCurrentWeekDayAgendaItems(
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Monday].Month,
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Monday].Day)
                .Where(x => x.FinishDate.Year == weekObject.SelectedWeek[defaultIndex].Year)
                .OrderBy(x=>x.FinishDate);

            lstBoxTuesday.ItemsSource = agendaViewerManager.GetCurrentWeekDayAgendaItems(
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Tuesday].Month,
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Tuesday].Day)
                .Where(x => x.FinishDate.Year == weekObject.SelectedWeek[defaultIndex].Year)
                .OrderBy(x => x.FinishDate);

            lstBoxWednsday.ItemsSource = agendaViewerManager.GetCurrentWeekDayAgendaItems(
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Wednsday].Month,
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Wednsday].Day)
                .Where(x => x.FinishDate.Year == weekObject.SelectedWeek[defaultIndex].Year)
                .OrderBy(x => x.FinishDate);

            lstBoxThursday.ItemsSource = agendaViewerManager.GetCurrentWeekDayAgendaItems(
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Thursday].Month,
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Thursday].Day)
                .Where(x => x.FinishDate.Year == weekObject.SelectedWeek[defaultIndex].Year)
                .OrderBy(x => x.FinishDate);

            lstBoxFriday.ItemsSource = agendaViewerManager.GetCurrentWeekDayAgendaItems(
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Friday].Month, 
                weekObject.SelectedWeek[(int)DayOfWeekEnum.Friday].Day)
                .Where(x=>x.FinishDate.Year == weekObject.SelectedWeek[defaultIndex].Year)
                .OrderBy(x => x.FinishDate);

            lstBoxSidePanel.ItemsSource = agendaViewerManager.GetNotCompletedAgendaItems(DateTime.UtcNow.Month);
        }

        private void txtListGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var context = (Grid)sender;
            var agendaItem = context.DataContext;

            AgendaItemWindow agendaWindow = new AgendaItemWindow((AgendaItem)agendaItem, selectedLanguage);

            agendaWindow.DataChanged += AgendaItemWindow_DataChanged;
            
            agendaWindow.Show();
        }

        private void AgendaItemWindow_DataChanged(object sender, EventArgs e)
        {
            UpdateWeeklyAgendaList();
        }
    }
} 