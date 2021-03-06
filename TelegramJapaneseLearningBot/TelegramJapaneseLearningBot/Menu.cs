﻿using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramJapaneseLearningBot
{
    public class Menu
    {
        private InlineKeyboardMarkup KeyboardMarkup { get; set; }
        public List<InlineKeyboardButton> Buttons { get; set; }

        public async void CreateMenu(TelegramBotClient botClient, Chat chat, string message)
        {
            try
            {
                KeyboardMarkup = new InlineKeyboardMarkup(Buttons);
                await botClient.SendTextMessageAsync(chat.Id, message, ParseMode.Default, false, false, 0, KeyboardMarkup);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}