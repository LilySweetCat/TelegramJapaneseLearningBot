using System;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;
using TelegramJapaneseLearningBot.DBContext.Models;

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
            try
            {
                await _context.Users.AddAsync(new LearningUser
                {
                    Username = e.CallbackQuery.From.Username,
                    LearningUserSettings = new LearningUserSettings()
                    {
                        Interval = TimeSpan.FromHours(12),
                        IsSpeechTraining = false,
                        IsTextTraining = true
                    }
                });

                await _context.SaveChangesAsync();

                await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Вы зарегистрированы {e.CallbackQuery.From.Username}",
                    true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public string Name { get; set; }
        public Action<CallbackQueryEventArgs> Handler { get; set; }
    }
}