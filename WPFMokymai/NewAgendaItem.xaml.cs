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
using System.Windows.Shapes;
using AgendaApp.BL.Models;
using AgendaApp.BL.Services;
using AgendaApp.DL.Models;
using AgendaApp.BL;

namespace AgendaApp
{
    /// <summary>
    /// Interaction logic for NewAgendaItem.xaml
    /// </summary>
    public partial class NewAgendaItem : Window
    {
        private const string windowName = "AgendaWindow";
        private const int defaultIndex = 0;

        AgendaManager agendaManager { get; }
        UiMessagesService uiMessagesService { get; }
        
        TimeObject TimeObj { get; set; }
        AgendaItem AgendaItem { get; set; }
        TranslationsAgendaObject TranslationsAgendaObject { get; set; }
        PriorityItem priorityItem { get; set; }
        public string SelectedLanguage { get; set; }


        public NewAgendaItem(string language)
        {
            InitializeComponent();

            SelectedLanguage = language;
            TimeObj = new TimeObject();
            AgendaItem = new AgendaItem();
            uiMessagesService = new UiMessagesService(language);
            agendaManager = new AgendaManager();
  
            TranslateWindowText();

            lstBoxStartingDayHours.ItemsSource = TimeObj.Hours;
            lstBoxStartingDayMinutes.ItemsSource = TimeObj.Minutes;
            lstBoxFinishDayHours.ItemsSource = TimeObj.Hours;
            lstBoxFinishDayMinutes.ItemsSource = TimeObj.Minutes;
            cmbBoxPriority.SelectedIndex = defaultIndex;
        }

        public virtual void TranslateWindowText()
        {
            TranslationsAgendaObject = (TranslationsAgendaObject)uiMessagesService.GetTranslationsObject(windowName);

            Title = TranslationsAgendaObject.WindowTitle;
            lblFinishDay.Content = TranslationsAgendaObject.Days;
            cldFinishDay.Text = TranslationsAgendaObject.DaySelector;
            lblFinishDayHours.Content = TranslationsAgendaObject.Hours;
            lblFinishDayMinutes.Content = TranslationsAgendaObject.Minutes;
            lblTitle.Content = TranslationsAgendaObject.Title;
            lblDescription.Content = TranslationsAgendaObject.Description;
            btnSave.Content = TranslationsAgendaObject.ItemSaveButtonText;
            btnExit.Content = TranslationsAgendaObject.ItemExitButtonText;
            cmbBoxPriority.ItemsSource = TranslationsAgendaObject.PriorityList;
            lblPriority.Content = TranslationsAgendaObject.Priority;
            lblStartDayTitle.Content = TranslationsAgendaObject.StartDay;
            lblFinishDayTitle.Content = TranslationsAgendaObject.FinishDay;

            lblStartingDay.Content = TranslationsAgendaObject.Days;
            lblStartingDayMinutes.Content = TranslationsAgendaObject.Minutes;
            lblStartingDayHours.Content = TranslationsAgendaObject.Hours;
        }

        private void listBoxMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ListBox)sender;

            if (selected.Name == lstBoxStartingDayMinutes.Name)
            {
                TimeObj.selectedStartingMin = Convert.ToInt32(selected.SelectedItem);
                TimeObj.IsStartingDayMinutesSelected = true;
            }
            else if (selected.Name == lstBoxFinishDayMinutes.Name)
            {
                TimeObj.selectedFinishMin = Convert.ToInt32(selected.SelectedItem);
                TimeObj.IsFinishDayMinutesSelected = true;
            }
            selected.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }
        private void listBoxHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = sender as ListBox;
            
            if(selected.Name == lstBoxStartingDayHours.Name)
            {
                TimeObj.selectedStartingHour = Convert.ToInt32(selected.SelectedItem);
                TimeObj.IsStartingDayHourSelected = true;
            }
            else if (selected.Name == lstBoxFinishDayHours.Name)
            {
                TimeObj.selectedFinishHour = Convert.ToInt32(selected.SelectedItem);
                TimeObj.IsFinishDayHourSelected = true;
            }
            selected.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }

        private void cldSample_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker list = (DatePicker)sender;
         
            if(list.Name == cldStartingDay.Name)
            {
                TimeObj.StartingDay = (DateTime)list.SelectedDate;
                TimeObj.IsStartingDayDaySelected = true;
            }
            else if(list.Name == cldFinishDay.Name)
            {
                TimeObj.FinishDay = (DateTime)list.SelectedDate;
                TimeObj.IsFinishDayDaySelected = true;
            }
            list.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }

        private void btn_exitWithoutSaving(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_saveAndExit(object sender, RoutedEventArgs e)
        {
            CreateAgendaObject();

            this.Close();
        }

        public virtual void CreateAgendaObject()
        {
            AgendaItem.IsCompleted = false;
            AgendaItem.Priority = priorityItem.Id;

            agendaManager.CreateAgenda(AgendaItem);
        }

        private void CreateFinishDate()
        {
            if(TimeObj.IsFinishDayDaySelected && TimeObj.IsFinishDayHourSelected && TimeObj.IsFinishDayMinutesSelected)
                AgendaItem.FinishDate = new DateTime(
                    TimeObj.FinishDay.Year,
                    TimeObj.FinishDay.Month,
                    TimeObj.FinishDay.Day,
                    TimeObj.selectedFinishHour,
                    TimeObj.selectedFinishMin,
                    defaultIndex);
        }

        private void CreateStartingDate()
        {
            if (TimeObj.IsStartingDayDaySelected && TimeObj.IsStartingDayHourSelected && TimeObj.IsStartingDayMinutesSelected)
                AgendaItem.StartDate = new DateTime(
                    TimeObj.StartingDay.Year,
                    TimeObj.StartingDay.Month,
                    TimeObj.StartingDay.Day,
                    TimeObj.selectedStartingHour,
                    TimeObj.selectedStartingMin,
                    defaultIndex);
                    }

        private void CheckIfAgendaItemIsCompleted()
        {
            if(TimeObj.IsFinishDayDaySelected && TimeObj.IsFinishDayHourSelected && TimeObj.IsFinishDayMinutesSelected)
            {
                CreateFinishDate();
            }
            if(TimeObj.IsStartingDayDaySelected && TimeObj.IsStartingDayHourSelected && TimeObj.IsStartingDayMinutesSelected)
            {
                CreateStartingDate();

            }
            if (AgendaItem.Description != null && AgendaItem.Title != null && CheckIfDatesAreCorrect())
                btnSave.IsEnabled = true;
            else btnSave.IsEnabled = false;
        }

        private bool CheckIfDatesAreCorrect()
        {
            return AgendaItem.StartDate < AgendaItem.FinishDate ? true : false;
        }

        private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxObj = (TextBox)sender;
            
            if (textBoxObj.Text.Length > 0)
            {
                textBoxObj.BorderBrush = new SolidColorBrush(Colors.White);
                AgendaItem.Title = textBoxObj.Text;
            }
            else
            {
                textBoxObj.BorderBrush = new SolidColorBrush(Colors.Red);
                AgendaItem.Title = null;
            }
            CheckIfAgendaItemIsCompleted();
        }

        private void descriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxObj = (TextBox)sender;

            if (textBoxObj.Text.Length > 0)
            {
                textBoxObj.BorderBrush = new SolidColorBrush(Colors.White);
                AgendaItem.Description = textBoxObj.Text;
            }
            else
            {
                textBoxObj.BorderBrush = new SolidColorBrush(Colors.Red);
                AgendaItem.Description = null;
            }
            CheckIfAgendaItemIsCompleted();
        }

        private void cmbBoxPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBox = (ComboBox)sender;
            priorityItem = (PriorityItem)selectedBox.SelectedItem;
            AgendaItem.Priority = priorityItem.Id;
        }
    }
}
