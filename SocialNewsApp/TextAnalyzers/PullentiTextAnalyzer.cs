using Pullenti.Ner;
using SocialNewsApp.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNewsApp.TextAnalyzers
{
    /// <summary>
    /// Анализатор текста Pullenti
    /// </summary>
    public class PullentiTextAnalyzer : ITextAnalyzer
    {
        private Processor _processor;

        static PullentiTextAnalyzer()
        {
            Pullenti.Sdk.InitializeAll();
        }

        public PullentiTextAnalyzer()
        {
            _processor = ProcessorService.CreateProcessor();
        }

        /// <summary>
        /// Метод получения списка ключевых слов по тексту
        /// </summary>
        /// <param name="text">Текст для поиска ключевых слов</param>
        public IEnumerable<string> Analyze(string text)
        {
            var result = _processor.Process(new SourceOfAnalysis(text));

            var entitiesTypes = new string[] { "GEO", "ORGANIZATION", "PERSON", "DECREE", "NAMEDENTITY" };
            return result.Entities.Where(p => entitiesTypes.Contains(p.TypeName)).Select(p => p.ToString());
        }
    }
}