using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class ManualLessonHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly Wiktionary _wiktionary;
        private readonly TelegramBotClient _botClient;

        public ManualLessonHandler(Wiktionary wiktionary, TelegramBotClient botClient)
        {
            _wiktionary = wiktionary;
            _botClient = botClient;
        }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            var word = await _wiktionary.GetRandomDictionaryWord();

            await _botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat,
                $"Kanji: {word.Word}{Environment.NewLine}Hiragana: {word.Hiragana}{Environment.NewLine}Romanji: {word.Romanji}{Environment.NewLine}Translations {word.Translations.FirstOrDefault()}");
        }
    }
}