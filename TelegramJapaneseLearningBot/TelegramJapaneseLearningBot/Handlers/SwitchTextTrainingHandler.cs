using System;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class SwitchTextTrainingHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly Context _context;
        private readonly TelegramBotClient _botClient;

        public SwitchTextTrainingHandler(Context context, TelegramBotClient botClient)
        {
            _context = context;
            _botClient = botClient;
            Name = nameof(SwitchTextTrainingHandler);
        }

        public string Name { get; }
        public Action<CallbackQueryEventArgs> Handler { get; set; }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            var userContext = await _context.Users.FirstOrDefaultAsync(user => user.Username == e.CallbackQuery.From.Username);
            var setting =
                await _context.Settings.FirstOrDefaultAsync(s => s.LearningUserId == userContext.LearningUserId);
            setting.IsTextTraining = !setting.IsTextTraining;
            await _context.SaveChangesAsync();
            await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Вы успешно установили текстовый режим",
                true);
        }
    }
}