using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace TelegramJapaneseLearningBot
{
    public class Wiktionary
    {
        private readonly IConfiguration _configuration;

        public Wiktionary(IConfiguration configuration) => _configuration = configuration;

        public async Task<DictionaryWord> GetRandomDictionaryWord()
        {
            var link = _configuration.GetValue<string>("DictionaryResouces:Wikidictionary");

            var word = new DictionaryWord();

            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(link);

            word.Word = document.All.FirstOrDefault(m => m.LocalName == "h1").Text();
            word.Kanjis = document.All.Where(m => m.LocalName == "table" && m.ClassList != null && m.ClassList.Contains("kanji-table")).Select(x => $"{x.Children.Select(d => d.Text())}");
            word.Hiragana = document.All
                .FirstOrDefault(m => m.LocalName == "b" && m.ClassList != null && m.ClassList.Contains("kana-noun-form-of")).Text();
            word.Romanji = document.All.FirstOrDefault(m => m.LocalName == "b" && m.ClassList != null && m.ClassList.Contains("Latn")).Text();
            word.Translations = document.All.FirstOrDefault(m => m.LocalName == "ol")?.Children.Select(x => x.Text());

            return word;
        }
    }
}