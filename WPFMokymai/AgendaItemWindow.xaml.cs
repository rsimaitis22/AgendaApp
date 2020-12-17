using AgendaApp.BL.Models;
using AgendaApp.BL.Services;
using AgendaApp.DL.Models;
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

namespace WPFMokymai
{
    /// <summary>
    /// Interaction logic for AgendaItem.xaml
    /// </summary>
    public partial class AgendaItemWindow : Window
    {
        private const string windowName = "AgendaWindow";
        UiMessagesService uiMessagesService { get; }
        AgendaManager agendaManager { get; }

        TranslationsAgendaObject TranslationsAgendaObject { get; set; }
        AgendaItem AgendaItem { get; set; }

        public AgendaItemWindow(AgendaItem agenda,string translationLanguage)
        {
            agendaManager = new AgendaManager();
            uiMessagesService = new UiMessagesService(translationLanguage);
            AgendaItem = new AgendaItem();

            InitializeComponent();
            TranslateWindowText();

            AgendaItem = agenda;
            lblAgendaItemTitle.Content = agenda.Title;
            lblAgendaItemDescription.Content = agenda.Description;
        }

        public virtual void TranslateWindowText()
        {
            TranslationsAgendaObject = (TranslationsAgendaObject)uiMessagesService.GetTranslationsObject(windowName);
            lblTitle.Content = TranslationsAgendaObject.Title;
            lblDescription.Content = TranslationsAgendaObject.Description;
            btnExit.Content = TranslationsAgendaObject.ItemExitButtonText;
            btnMarkCompleted.Content = TranslationsAgendaObject.MarkCompleted;
        }

        private void btnMarkCompleted_Click(object sender, RoutedEventArgs e)
        {
            AgendaItem.IsCompleted = true;
            agendaManager.ModifyAgendaItem(AgendaItem);
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
