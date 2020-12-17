namespace AgendaApp.BL.Interfaces
{
    public interface IUiMessagesService
    {
        string TranslationLanguage { get; }

        object GetTranslationsObject(string windowName);
        void InitializeTranslationObjects();
        string ReadDataFromFile();
    }
}