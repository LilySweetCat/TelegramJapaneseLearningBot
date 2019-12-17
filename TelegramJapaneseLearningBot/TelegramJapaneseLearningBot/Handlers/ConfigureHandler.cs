using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class ConfigureHandler : IHandler<CallbackQueryEventArgs>
    {
        private readonly TelegramBotClient _client;
        private readonly Context _context;

        public ConfigureHandler(TelegramBotClient client, Context context)
        {
            _client = client;
            _context = context;
            Name = nameof(ConfigureHandler);
        }

        public string Name { get; }

        public async void OnHandler(CallbackQueryEventArgs e)
        {
            var menu = new Menu
            {
                Buttons = new List<InlineKeyboardButton>
                {
                    new InlineKeyboardButton
                    {
                        Text = "Установить интервал занятий", CallbackData = nameof(ChangeIntervalHandler)
                    }
                }
            };
            var user = await _context.Users.FirstOrDefaultAsync(userContext =>
                userContext.Username == e.CallbackQuery.From.Username);

            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.LearningUserId == user.LearningUserId);

            if (setting.IsSpeechTraining)
                menu.Buttons.Add(new InlineKeyboardButton
                {
                    Text = "Включить устные занятия",
                    CallbackData = nameof(SwitchSpeechTrainingHandler)
                });
            else
                menu.Buttons.Add(new InlineKeyboardButton
                {
                    Text = "Выключить устные занятия",
                    CallbackData = nameof(SwitchSpeechTrainingHandler)
                });
            if (setting.IsTextTraining)
                menu.Buttons.Add(new InlineKeyboardButton
                {
                    Text = "Включить письменные занятия",
                    CallbackData = nameof(SwitchTextTrainingHandler)
                });
            else
                menu.Buttons.Add(new InlineKeyboardButton
                {
                    Text = "Выключить письменные занятия",
                    CallbackData = nameof(SwitchTextTrainingHandler)
                });

            menu.CreateMenu(_client, e.CallbackQuery.Message.Chat, "Выберите настройку");
        }
    }
}