using System;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class SwitchSpeechTrainingHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly Context _context;

        public SwitchSpeechTrainingHandler(Context context)
        {
            _context = context;
            Name = nameof(SwitchSpeechTrainingHandler);
        }

        public string Name { get; }
        public Action<CallbackQueryEventArgs> Handler { get; set; }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            var userContext = await _context.Users.FirstOrDefaultAsync(user => user.UserId == e.CallbackQuery.From.Id);
            userContext.UserSettings.IsSpeechTraining = !userContext.UserSettings.IsSpeechTraining;
            await _context.SaveChangesAsync();
        }
    }
}