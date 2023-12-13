namespace SocialNewsApp.Model
{
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