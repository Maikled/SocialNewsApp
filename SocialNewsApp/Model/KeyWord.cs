namespace SocialNewsApp.Model
{
    /// <summary>
    /// Класс ключевого слова
    /// </summary>
    public class KeyWord
    {
        public string Word { get; }
        public bool IsSelected { get; set; }

        public KeyWord(string word)
        {
            Word = word;
        }
    }
}