using AgendaApp.BL.Interfaces;
using AgendaApp.BL.Models;
using AgendaApp.BL.Services;
using AgendaApp.DL.Models;
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

        public AgendaItemWindow(AgendaItem agenda,string translationLanguage)
        {
            agendaManager = new AgendaManager();
            uiMessagesService = new UiMessagesService(translationLanguage);
            agendaItem = new AgendaItem();

            InitializeComponent();
            TranslateWindowText();

            agendaItem = agenda;
            lblAgendaItemTitle.Content = agenda.Title;
            lblAgendaItemDescription.Content = agenda.Description;
        }

        public virtual void TranslateWindowText()
        {
            translationsAgendaObject = (TranslationsAgendaObject)uiMessagesService.GetTranslationsObject(windowName);
            lblTitle.Content = translationsAgendaObject.Title;
            lblDescription.Content = translationsAgendaObject.Description;
            btnExit.Content = translationsAgendaObject.ItemExitButtonText;
            btnMarkCompleted.Content = translationsAgendaObject.MarkCompleted;
        }

        private void btnMarkCompleted_Click(object sender, RoutedEventArgs e)
        {
            agendaItem.IsCompleted = true;
            agendaManager.ModifyAgendaItem(agendaItem);
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
