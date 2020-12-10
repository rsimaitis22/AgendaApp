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

namespace AgendaApp
{
    /// <summary>
    /// Interaction logic for NewAgendaItem.xaml
    /// </summary>
    public partial class NewAgendaItem : Window
    {
        TimeObject TimeObj { get; set; }
        UiMessagesService uiMessagesService { get; }
        AgendaManager agendaManager { get; }
        AgendaItem AgendaItem { get; set; }
        TranslationsAgendaObject TranslationsAgendaObject { get; set; }

        public NewAgendaItem(string language)
        {
            InitializeComponent();

            uiMessagesService = new UiMessagesService(language);
            TimeObj = new TimeObject();
            AgendaItem = new AgendaItem();
            agendaManager = new AgendaManager();

            TranslateWindowText();

            listBoxMinutes.ItemsSource = TimeObj.Minutes;
            listBoxHours.ItemsSource = TimeObj.Hours;
        }

        private void TranslateWindowText()
        {
            TranslationsAgendaObject = uiMessagesService.GetAgendaObjectTranslations();
            Title = TranslationsAgendaObject.AgendaWindowTitle;
            dayLabel.Content = TranslationsAgendaObject.AgendaDays;
            cldSample.Text = TranslationsAgendaObject.AgendaDaySelector;
            hoursLabel.Content = TranslationsAgendaObject.AgendaHours;
            minutesLabel.Content = TranslationsAgendaObject.AgendaMinutes;
            titleLabel.Content = TranslationsAgendaObject.AgendaTitle;
            descriptionLabel.Content = TranslationsAgendaObject.AgendaDescription;
            saveButton.Content = TranslationsAgendaObject.AgendaItemSaveButtonText;
            exitButton.Content = TranslationsAgendaObject.AgendaItemExitButtonText;
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

            this.Close();
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
