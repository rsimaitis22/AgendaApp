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
using System.Windows.Media;
using Microsoft.Win32;
using AgendaApp.BL;

namespace AgendaApp
{
    public partial class MainWindow : Window
    {
        private string windowName;
        private const int defaultIndex = 0;

        IUiMessagesService messagesService;
        IAgendaViewerManager agendaViewerManager;

        TranslationsMainWindowObject translationsMainWindowObject;
        WeekObj weekObject;
        string selectedLanguage;

        public MainWindow()
        {
            windowName = WindowNamesEnum.MainWindow.ToString();

            agendaViewerManager = new AgendaViewerManager();
            weekObject = new WeekObj();

            InitializeComponent();
            InitializeLanguage();
            TranslateWindowText();

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

            if(item.Count > 1)
                agendaViewerManager.AddMultipleNewlyCreatedAgendas(DateTime.Now.Month, item);
            else
                agendaViewerManager.AddNewlyCreatedAgenda(item.FirstOrDefault().FinishDate.Month);
                        
            UpdateWeeklyAgendaList();
        }

        private void sidePanelScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta );
            e.Handled = true;
        }
        public void TranslateWindowText()
        {
            translationsMainWindowObject = (TranslationsMainWindowObject)messagesService.GetTranslationsObject(windowName);

            Title = translationsMainWindowObject.Title;
            btnCreateNewAgenda.Content = translationsMainWindowObject.NewAgendaButtonText;
            chkBoxShowNearest.Content = translationsMainWindowObject.Nearest;
            lblMonday.Content = translationsMainWindowObject.Monday;
            lblTuesday.Content = translationsMainWindowObject.Tuesday;
            lblWednesday.Content = translationsMainWindowObject.Wednesday;
            lblThursday.Content = translationsMainWindowObject.Thursday;
            lblFriday.Content = translationsMainWindowObject.Friday;
            lblWeekName.Content = translationsMainWindowObject.Week;
            btnNextWeek.Content = translationsMainWindowObject.Next;
            btnPreviousWeek.Content = translationsMainWindowObject.Previous;
        }

        private void chkBoxShowNearest_Checked(object sender, RoutedEventArgs e)
        {
            var check = (CheckBox)sender;

            lstBoxSidePanel.ItemsSource = (bool)check.IsChecked ? 
                agendaViewerManager.GetFutureNearestMonthAgendaItems(DateTime.UtcNow.Month) : 
                agendaViewerManager.GetNotCompletedAgendaItems(DateTime.UtcNow.Month);
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
            lblWeekNumber.Content = weekObject.GetCurrentWeekNumber();

            lstBoxMonday.ItemsSource = FilterCurrentWeekAgenda(DayOfWeekEnum.Monday);
            lstBoxTuesday.ItemsSource = FilterCurrentWeekAgenda(DayOfWeekEnum.Tuesday);
            lstBoxWednsday.ItemsSource = FilterCurrentWeekAgenda(DayOfWeekEnum.Wednesday);
            lstBoxThursday.ItemsSource = FilterCurrentWeekAgenda(DayOfWeekEnum.Thursday);
            lstBoxFriday.ItemsSource = FilterCurrentWeekAgenda(DayOfWeekEnum.Friday);
            lstBoxSaturday.ItemsSource = FilterCurrentWeekAgenda(DayOfWeekEnum.Saturday);
            lstBoxSunday.ItemsSource = FilterCurrentWeekAgenda(DayOfWeekEnum.Sunday);

            lstBoxSidePanel.ItemsSource = agendaViewerManager.GetNotCompletedAgendaItems(DateTime.UtcNow.Month);
        }

        private List<AgendaItem> FilterCurrentWeekAgenda(DayOfWeekEnum day)
        {
            return agendaViewerManager.GetCurrentWeekDayAgendaItems(
                            weekObject.SelectedWeek[(int)day].Month,
                            weekObject.SelectedWeek[(int)day].Day)
                .Where(x => x.FinishDate.Year == weekObject.SelectedWeek[defaultIndex].Year)
                .OrderBy(x => x.FinishDate).ToList(); 
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