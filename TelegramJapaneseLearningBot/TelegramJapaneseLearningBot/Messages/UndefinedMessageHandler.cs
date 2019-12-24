using System.Collections.Generic;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramJapaneseLearningBot.DBContext;
using TelegramJapaneseLearningBot.Handlers;

namespace TelegramJapaneseLearningBot.Messages
{
    public class UndefinedMessageHandler : IHandler<MessageEventArgs>
    {
        private readonly TelegramBotClient _client;
        private readonly ILifetimeScope _scope;

        public UndefinedMessageHandler(TelegramBotClient client, ILifetimeScope scope)
        {
            _client = client;
            _scope = scope;
        }

        public async void OnHandler(MessageEventArgs e)
        {
            using var scope = _scope.BeginLifetimeScope();

            var context = scope.Resolve<Context>();

            var isUser = await context.Users.AnyAsync(user => user.Username == e.Message.From.Username);
            var buttons = new List<InlineKeyboardButton>();
            if (!isUser)
                buttons.Add(new InlineKeyboardButton
                {
                    Text = "Зарегистрироваться?", CallbackData = nameof(RegisterHandler)
                });
            else
                buttons.AddRange(new[]
                {
                    new InlineKeyboardButton
                    {
                        Text = "Настройки", CallbackData = nameof(ConfigureHandler)
                    },
                    new InlineKeyboardButton()
                    {
                        Text = "Ручное занятие", CallbackData = nameof(ManualLessonHandler)
                    },
                    new InlineKeyboardButton
                    {
                        Text = "Отписаться",
                        CallbackData = nameof(UnregisterHandler)
                    }
                });

            var menu = new Menu
            {
                Buttons = buttons
            };

            menu.CreateMenu(_client, e.Message.Chat, "Выберите настройку");
        }
    }
}