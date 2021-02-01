using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AgendaApp.BL.Models;
using AgendaApp.BL.Services;
using AgendaApp.DL.Models;
using AgendaApp.BL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using AgendaApp.BL;

namespace AgendaApp
{
    public partial class NewAgendaItem : Window
    {
        private string windowName;
        private const int defaultIndex = 0;

        IAgendaManager agendaManager;
        IUiMessagesService uiMessagesService;

        List<DateTime> repeatingDates;
        List<DateTime> selectedDates;
        List<AgendaItem> createdAgendas;

        TimeObject timeObj;
        AgendaItem agendaItem;
        TranslationsAgendaObject translationsAgendaObject;
        PriorityItem priorityItem;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler DataChanged;

        public NewAgendaItem(string language)
        {
            windowName = WindowNamesEnum.AgendaWindow.ToString();
            InitializeComponent();

            createdAgendas = new List<AgendaItem>();
            repeatingDates = new List<DateTime>();
            selectedDates = new List<DateTime>();
            timeObj = new TimeObject();
            agendaItem = new AgendaItem();
            agendaItem.StartDate = null;
            uiMessagesService = new UiMessagesService(language);
            agendaManager = new AgendaManagerCSV();

            repeatingDates = GetDatesInMonthList(DateTime.UtcNow.Month);
            TranslateWindowText();

            lstBoxStartingDayHours.ItemsSource = timeObj.Hours;
            lstBoxStartingDayMinutes.ItemsSource = timeObj.Minutes;
            lstBoxFinishDayHours.ItemsSource = timeObj.Hours;
            lstBoxFinishDayMinutes.ItemsSource = timeObj.Minutes;
            cmbBoxPriority.SelectedIndex = defaultIndex;
        }

        public void TranslateWindowText()
        {
            translationsAgendaObject = (TranslationsAgendaObject)uiMessagesService.GetTranslationsObject(windowName);

            Title = translationsAgendaObject.WindowTitle;
            lblFinishDay.Content = translationsAgendaObject.Days;
            cldFinishDay.Text = translationsAgendaObject.DaySelector;
            lblFinishDayHours.Content = translationsAgendaObject.Hours;
            lblFinishDayMinutes.Content = translationsAgendaObject.Minutes;
            lblTitle.Content = translationsAgendaObject.Title;
            lblDescription.Content = translationsAgendaObject.Description;
            btnSave.Content = translationsAgendaObject.ItemSaveButtonText;
            btnExit.Content = translationsAgendaObject.ItemExitButtonText;
            cmbBoxPriority.ItemsSource = translationsAgendaObject.PriorityList;
            lblPriority.Content = translationsAgendaObject.Priority;
            lblStartDayTitle.Content = translationsAgendaObject.StartDay;
            lblFinishDayTitle.Content = translationsAgendaObject.FinishDay;

            lblStartingDay.Content = translationsAgendaObject.Days;
            lblStartingDayMinutes.Content = translationsAgendaObject.Minutes;
            lblStartingDayHours.Content = translationsAgendaObject.Hours;

            lblMonday.Content = translationsAgendaObject.Monday.First();
            lblTuesday.Content = translationsAgendaObject.Tuesday.First();
            lblWednesday.Content = translationsAgendaObject.Wednesday.First();
            lblThursday.Content = translationsAgendaObject.Thursday.First();
            lblFriday.Content = translationsAgendaObject.Friday.First();
            lblSaturday.Content = translationsAgendaObject.Saturday.First();
            lblSunday.Content = translationsAgendaObject.Sunday.First();
        }

        private void listBoxMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ListBox)sender;

            if (selected.Name == lstBoxStartingDayMinutes.Name)
            {
                timeObj.selectedStartingMin = Convert.ToInt32(selected.SelectedItem);
                timeObj.IsStartingDayMinutesSelected = true;
            }
            else if (selected.Name == lstBoxFinishDayMinutes.Name)
            {
                timeObj.selectedFinishMin = Convert.ToInt32(selected.SelectedItem);
                timeObj.IsFinishDayMinutesSelected = true;
            }
            selected.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }
        private void listBoxHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ListBox)sender;
            
            if(selected.Name == lstBoxStartingDayHours.Name)
            {
                timeObj.selectedStartingHour = Convert.ToInt32(selected.SelectedItem);
                timeObj.IsStartingDayHourSelected = true;
            }
            else if (selected.Name == lstBoxFinishDayHours.Name)
            {
                timeObj.selectedFinishHour = Convert.ToInt32(selected.SelectedItem);
                timeObj.IsFinishDayHourSelected = true;
            }
            selected.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }

        private void cldSample_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker list = (DatePicker)sender;
         
            if(list.Name == cldStartingDay.Name)
            {
                timeObj.StartingDay = (DateTime)list.SelectedDate;
                timeObj.IsStartingDayDaySelected = true;
            }
            else if(list.Name == cldFinishDay.Name)
            {
                timeObj.FinishDay = (DateTime)list.SelectedDate;
                timeObj.IsFinishDayDaySelected = true;
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
            if(selectedDates.Count == 0)
            {
                CreateAgendaObject();
            }
            else
            {
                CreateRepeatableAgendaObjects();
            }

            DataChangedEventHandler handler = DataChanged;
            if (handler != null)
            {
                handler(createdAgendas, new EventArgs());
            }

            this.Close();
        }

        public virtual void CreateAgendaObject()
        {
            agendaItem.IsRepeatable = false;
            agendaItem.IsCompleted = false;
            agendaItem.Priority = priorityItem.Id;

            createdAgendas.Add(agendaItem);

            agendaManager.CreateAgenda(agendaItem);
        }
        public virtual void CreateRepeatableAgendaObjects()
        {
            //TODO sutvarkyt 
            int lastAgendaId = agendaManager.GetNewlyCreatedAgenda().Id;
            agendaItem.IsCompleted = false;
            agendaItem.Priority = priorityItem.Id;
            agendaItem.Id = lastAgendaId+1;
            agendaItem.IsRepeatable = true;

            selectedDates = selectedDates
                .Where(x => x.Day >= agendaItem.StartDate.Value.Day 
                    && x.Day <= agendaItem.FinishDate.Day 
                    && x.Month == agendaItem.FinishDate.Month)
                .ToList();

            foreach (var singleDay in selectedDates)
            {
                agendaItem.FinishDate = singleDay.AddMinutes(timeObj.selectedFinishMin).AddHours(timeObj.selectedFinishHour);
                agendaManager.CreateAgenda(agendaItem);

                createdAgendas.Add(new AgendaItem()
                {
                    Id = agendaItem.Id,
                    Title = agendaItem.Title,
                    Description = agendaItem.Description,
                    StartDate = agendaItem.StartDate,
                    FinishDate = agendaItem.FinishDate,
                    IsCompleted = agendaItem.IsCompleted,
                    IsRepeatable = agendaItem.IsRepeatable,
                    RepeatableInterval = agendaItem.RepeatableInterval,
                    Priority = agendaItem.Priority
                });
            }
        }

        private void CreateFinishDate()
        {
            if(timeObj.IsFinishDayDaySelected && timeObj.IsFinishDayHourSelected && timeObj.IsFinishDayMinutesSelected)
                agendaItem.FinishDate = new DateTime(
                    timeObj.FinishDay.Year,
                    timeObj.FinishDay.Month,
                    timeObj.FinishDay.Day,
                    timeObj.selectedFinishHour,
                    timeObj.selectedFinishMin,
                    defaultIndex);
        }

        private void CreateStartingDate()
        {
            if (timeObj.IsStartingDayDaySelected && timeObj.IsStartingDayHourSelected && timeObj.IsStartingDayMinutesSelected)
                agendaItem.StartDate = new DateTime(
                    timeObj.StartingDay.Year,
                    timeObj.StartingDay.Month,
                    timeObj.StartingDay.Day,
                    timeObj.selectedStartingHour,
                    timeObj.selectedStartingMin,
                    defaultIndex);

            
        }

        private void CheckIfAgendaItemIsCompleted()
        {
            if (timeObj.IsFinishDayDaySelected && timeObj.IsFinishDayHourSelected && timeObj.IsFinishDayMinutesSelected)
            {
                CreateFinishDate();
            }
            if(timeObj.IsStartingDayDaySelected && timeObj.IsStartingDayHourSelected && timeObj.IsStartingDayMinutesSelected)
            {
                CreateStartingDate();
            }
            if (agendaItem.Description != null && agendaItem.Title != null && CheckIfDatesAreCorrect())
            {
                btnSave.IsEnabled = true;
            }
            else btnSave.IsEnabled = false;
        }

        private bool CheckIfDatesAreCorrect()
        {
            return agendaItem.StartDate < agendaItem.FinishDate;
        }

        private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxObj = (TextBox)sender;
            ReturnBorderBrush(textBoxObj);
            agendaItem.Title = textBoxObj.Text;

            CheckIfAgendaItemIsCompleted();
        }


        private void descriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxObj = (TextBox)sender;
            ReturnBorderBrush(textBoxObj);
            agendaItem.Description = textBoxObj.Text;

            CheckIfAgendaItemIsCompleted();
        }
        private void ReturnBorderBrush(TextBox textBoxObj)
        {
            textBoxObj.BorderBrush = textBoxObj.Text.Length > 0
                ? new SolidColorBrush(Colors.White)
                : new SolidColorBrush(Colors.Red);
        }

        private void cmbBoxPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBox = (ComboBox)sender;
            priorityItem = (PriorityItem)selectedBox.SelectedItem;
            agendaItem.Priority = priorityItem.Id;
        }

        private void chkBox_Checked(object sender, RoutedEventArgs e)
        {
            var obj = (CheckBox)sender;
            string box = "chkBox";

            if (obj.Name == $"{box}Monday")
            {
                selectedDates.AddRange(repeatingDates.Where(x => x.DayOfWeek == DayOfWeek.Monday).ToList());
            }
            if (obj.Name == $"{box}Tuesday")
            {
                selectedDates.AddRange(repeatingDates.Where(x => x.DayOfWeek == DayOfWeek.Tuesday).ToList());
            }
            if (obj.Name == $"{box}Wednesday")
            {
                selectedDates.AddRange(repeatingDates.Where(x => x.DayOfWeek == DayOfWeek.Wednesday).ToList());
            }
            if (obj.Name == $"{box}Thursday")
            {
                selectedDates.AddRange(repeatingDates.Where(x => x.DayOfWeek == DayOfWeek.Thursday).ToList());
            }
            if (obj.Name == $"{box}Friday")
            {
                selectedDates.AddRange(repeatingDates.Where(x => x.DayOfWeek == DayOfWeek.Friday).ToList());
            }
            if (obj.Name == $"{box}Saturday")
            {
                selectedDates.AddRange(repeatingDates.Where(x => x.DayOfWeek == DayOfWeek.Saturday).ToList());
            }
            if (obj.Name == $"{box}Sunday")
            {
                selectedDates.AddRange(repeatingDates.Where(x => x.DayOfWeek == DayOfWeek.Sunday).ToList());
            }
        }
        private void chkBox_UnChecked(object sender, RoutedEventArgs e)
        {
            var obj = (CheckBox)sender;
            string box = "chkBox";

            if (obj.Name == $"{box}Monday")
            {
                selectedDates.RemoveAll(x => x.DayOfWeek == DayOfWeek.Monday);
            }
            if (obj.Name == $"{box}Tuesday")
            {
                selectedDates.RemoveAll(x => x.DayOfWeek == DayOfWeek.Tuesday);
            }
            if (obj.Name == $"{box}Wednesday")
            {
                selectedDates.RemoveAll(x => x.DayOfWeek == DayOfWeek.Wednesday);
            }
            if (obj.Name == $"{box}Thursday")
            {
                selectedDates.RemoveAll(x => x.DayOfWeek == DayOfWeek.Thursday);
            }
            if (obj.Name == $"{box}Friday")
            {
                selectedDates.RemoveAll(x => x.DayOfWeek == DayOfWeek.Friday);
            }
            if (obj.Name == $"{box}Saturday")
            {
                selectedDates.RemoveAll(x => x.DayOfWeek == DayOfWeek.Saturday);
            }
            if (obj.Name == $"{box}Sunday")
            {
                selectedDates.RemoveAll(x => x.DayOfWeek == DayOfWeek.Sunday);
            }
        }

        private List<DateTime> GetDatesInMonthList(int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(DateTime.UtcNow.Year, month)) 
                    .Select(day => new DateTime(DateTime.UtcNow.Year, month, day)) 
                    .ToList(); 
        }
    }
}
