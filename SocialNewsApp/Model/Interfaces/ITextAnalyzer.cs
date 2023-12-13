using System.Collections.Generic;

namespace SocialNewsApp.Model.Interfaces
{
    public interface ITextAnalyzer
    {
        public IEnumerable<string> Analyze(string text);
    }
}