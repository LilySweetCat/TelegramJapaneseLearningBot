using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;
using TelegramJapaneseLearningBot.Models;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class RegisterHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly TelegramBotClient _botClient;
        private readonly Context _context;

        public RegisterHandler(Context context, TelegramBotClient botClient)
        {
            _context = context;
            _botClient = botClient;
            Name = nameof(RegisterHandler);
        }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            await _context.Users.AddAsync(new User
            {
                UserId = e.CallbackQuery.Message.From.Id
            });
            await _context.SaveChangesAsync();

            await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Вы зарегистрированы {e.CallbackQuery.Data}",
                true);
        }

        public string Name { get; set; }
        public Action<CallbackQueryEventArgs> Handler { get; set; }
    }
}