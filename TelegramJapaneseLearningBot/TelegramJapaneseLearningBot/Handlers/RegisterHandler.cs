using System;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;
using TelegramJapaneseLearningBot.DBContext.Models;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class RegisterHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly ILifetimeScope _scope;
        private readonly TelegramBotClient _botClient;

        public RegisterHandler(ILifetimeScope scope, TelegramBotClient botClient)
        {
            _scope = scope;
            _botClient = botClient;
        }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            try
            {
                using var scope = _scope.BeginLifetimeScope();
                var context = scope.Resolve<Context>();

                await context.Users.AddAsync(new LearningUser
                {
                    Username = e.CallbackQuery.From.Username,
                    LearningUserSettings = new LearningUserSettings()
                    {
                        Interval = TimeSpan.FromHours(12),
                        IsSpeechTraining = false,
                        IsTextTraining = true
                    }
                });

                await context.SaveChangesAsync();

                await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Вы зарегистрированы {e.CallbackQuery.From.Username}",
                    true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}