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

        AgendaManager agendaManager { get; }
        UiMessagesService uiMessagesService { get; }
        TimeObject TimeObj { get; set; }
        AgendaItem AgendaItem { get; set; }
        TranslationsAgendaObject TranslationsAgendaObject { get; set; }

        public string SelectedLanguage { get; set; }


        public NewAgendaItem(string language)
        {
            InitializeComponent();

            SelectedLanguage = language;
            uiMessagesService = new UiMessagesService(language);
            TimeObj = new TimeObject();
            AgendaItem = new AgendaItem();
            agendaManager = new AgendaManager();

            TranslateWindowText();

            listBoxMinutes.ItemsSource = TimeObj.Minutes;
            listBoxHours.ItemsSource = TimeObj.Hours;
        }

        public virtual void TranslateWindowText()
        {
            TranslationsAgendaObject = (TranslationsAgendaObject)uiMessagesService.GetTranslationsObject(windowName);

            Title = TranslationsAgendaObject.WindowTitle;
            dayLabel.Content = TranslationsAgendaObject.Days;
            cldSample.Text = TranslationsAgendaObject.DaySelector;
            hoursLabel.Content = TranslationsAgendaObject.Hours;
            minutesLabel.Content = TranslationsAgendaObject.Minutes;
            titleLabel.Content = TranslationsAgendaObject.Title;
            descriptionLabel.Content = TranslationsAgendaObject.Description;
            saveButton.Content = TranslationsAgendaObject.ItemSaveButtonText;
            exitButton.Content = TranslationsAgendaObject.ItemExitButtonText;
        }

        private void listBoxMinutes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ListBox)sender;
            var s = selected.SelectedItem;
            TimeObj.selectedMin = Convert.ToInt32(s);

            listBoxMinutes.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }
        private void listBoxHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = sender as ListBox;
            var s = selected.SelectedItem;
            TimeObj.selectedHour = Convert.ToInt32(s);

            listBoxHours.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }

        private void cldSample_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker list = (DatePicker)sender;
            TimeObj.Date = (DateTime)list.SelectedDate;

            list.Background = new SolidColorBrush(Colors.GreenYellow);

            CheckIfAgendaItemIsCompleted();
        }

        private void btn_exitWithoutSaving(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_saveAndExit(object sender, RoutedEventArgs e)
        {
            SaveAgendaObject();

            this.Close();
        }

        public virtual void SaveAgendaObject()
        {
            DateTime dt = new DateTime(
                            cldSample.SelectedDate.Value.Year,
                            cldSample.SelectedDate.Value.Month,
                            cldSample.SelectedDate.Value.Day,
                            TimeObj.selectedHour,
                            TimeObj.selectedMin, 0);

            AgendaItem.FinishDate = dt;
            AgendaItem.StartDate = DateTime.UtcNow;
            AgendaItem.IsCompleted = false;

            agendaManager.CreateAgenda(AgendaItem);
        }

        private void CheckIfAgendaItemIsCompleted()
        {
            if(AgendaItem.Title != null && AgendaItem.Description != null)
            {
                saveButton.IsEnabled = true;
            }
        }

        private void titleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxObj = (TextBox)sender;
            
            if (textBoxObj.Text.Length > 0)
            {
                textBoxObj.Background = new SolidColorBrush(Colors.White);
                AgendaItem.Title = textBoxObj.Text;
            }
            else
            {
                textBoxObj.Background = new SolidColorBrush(Colors.PaleVioletRed);
            }
            CheckIfAgendaItemIsCompleted();
        }
        private void descriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxObj = (TextBox)sender;

            if (textBoxObj.Text.Length > 0)
            {
                textBoxObj.Background = new SolidColorBrush(Colors.White);
                AgendaItem.Description = textBoxObj.Text;
            }
            else
            {
                textBoxObj.Background = new SolidColorBrush(Colors.PaleVioletRed);
            }
            CheckIfAgendaItemIsCompleted();
        }
    }
}
