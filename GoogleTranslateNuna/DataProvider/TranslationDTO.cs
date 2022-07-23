
namespace GoogleTranslateNuna.DataProvider
{
    public class TranslationDTO
    {
        public string sourceLanguage { get; set; }
        public string targetLanguage { get; set; }
        public string initialText { get; set; }
        public string expectedText { get; set; }
        public string keyBoard { get; set; }
    }
}
