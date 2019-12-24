using System;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class UnregisterHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly ILifetimeScope _scope;
        private readonly TelegramBotClient _botClient;

        public UnregisterHandler(ILifetimeScope scope, TelegramBotClient botClient)
        {
            _scope = scope;
            _botClient = botClient;
        }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            using var scope = _scope.BeginLifetimeScope();
            var context = scope.Resolve<Context>();

            var userContext = await context.Users.FirstOrDefaultAsync(user => user.Username == e.CallbackQuery.From.Username);
            context.Users.Remove(userContext);
            await context.SaveChangesAsync();

            await _botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id,
                $"Вы успешно отписались {e.CallbackQuery.Data}",
                true);
        }
    }
}