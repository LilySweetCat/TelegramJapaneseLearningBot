using System;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class UnregisterHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly TelegramBotClient _botClient;
        private readonly Context _context;

        public UnregisterHandler(Context context, TelegramBotClient botClient)
        {
            _context = context;
            _botClient = botClient;
            Name = nameof(UnregisterHandler);
        }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            var userContext = await _context.Users.FirstOrDefaultAsync(user => user.Username == e.CallbackQuery.From.Username);
            _context.Users.Remove(userContext);
            await _context.SaveChangesAsync();

            await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id,
                $"Вы успешно отписались {e.CallbackQuery.Data}",
                true);
        }

        public string Name { get; }
        public Action<CallbackQueryEventArgs> Handler { get; set; }
    }
}