using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class ChangeIntervalHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly TelegramBotClient _botClient;

        public ChangeIntervalHandler(TelegramBotClient botClient)
        {
            _botClient = botClient;
            Name = nameof(ChangeIntervalHandler);
        }

        public string Name { get; }
        public Action<CallbackQueryEventArgs> Handler { get; set; }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            await _botClient.SendTextMessageAsync(e.CallbackQuery.ChatInstance, "Введите интервал занятий в часах: \\interval XX:XX");
        }
    }
}