using AgendaApp.BL.Interfaces;
using AgendaApp.BL.Models;
using AgendaApp.BL.Services;
using AgendaApp.DL.Models;
using System;
using System.Windows;

namespace WPFMokymai
{
    public partial class AgendaItemWindow : Window
    {
        private const string windowName = "AgendaWindow";

        IUiMessagesService uiMessagesService;
        IAgendaManager agendaManager;

        TranslationsAgendaObject translationsAgendaObject;
        AgendaItem agendaItem;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler DataChanged;

        public AgendaItemWindow(AgendaItem agenda,string translationLanguage)
        {
            agendaManager = new AgendaManager();
            uiMessagesService = new UiMessagesService(translationLanguage);
            agendaItem = new AgendaItem();

            InitializeComponent();

            agendaItem = agenda;
            lblAgendaItemTitle.Content = agenda.Title;
            lblAgendaItemDescription.Content = agenda.Description;

            TranslateWindowText();
        }

        public virtual void TranslateWindowText()
        {
            translationsAgendaObject = (TranslationsAgendaObject)uiMessagesService.GetTranslationsObject(windowName);
            lblTitle.Content = translationsAgendaObject.Title;
            lblDescription.Content = translationsAgendaObject.Description;
            btnExit.Content = translationsAgendaObject.ItemExitButtonText;
            btnMarkCompleted.Content = translationsAgendaObject.MarkCompleted;
            lblFinishTime.Content = $"{translationsAgendaObject.EstimatedFinish} : {agendaItem.FinishDate}";
        }

        private void btnMarkCompleted_Click(object sender, RoutedEventArgs e)
        {
            agendaItem.IsCompleted = true;
            agendaManager.ModifyAgendaItem(agendaItem);

            DataChangedEventHandler handler = DataChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }

            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
