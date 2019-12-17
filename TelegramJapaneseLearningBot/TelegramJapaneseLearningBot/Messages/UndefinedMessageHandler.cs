using System.Collections.Generic;
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
        private readonly Context _context;

        public UndefinedMessageHandler(TelegramBotClient client, Context context)
        {
            _client = client;
            _context = context;
            Name = nameof(UndefinedMessageHandler);
        }

        public string Name { get; }

        public async void OnHandler(MessageEventArgs e)
        {
            var isUser = await _context.Users.AnyAsync(user => user.Username == e.Message.From.Username);
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