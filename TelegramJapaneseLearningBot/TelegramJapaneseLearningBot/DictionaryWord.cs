using System.Collections.Generic;

namespace TelegramJapaneseLearningBot
{
    public class DictionaryWord
    {
        public string Word { get; set; }

        public string Hiragana { get; set; }

        public string Romanji { get; set; }

        public IEnumerable<string> Translations { get; set; }

        public IEnumerable<string> Kanjis { get; set; }
    }
}