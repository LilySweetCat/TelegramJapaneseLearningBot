using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot.Messages
{
    public class ChangeIntervalMessageHandler : IHandler<MessageEventArgs>
    {
        private readonly Context _context;
        private readonly TelegramBotClient _client;

        public ChangeIntervalMessageHandler(Context context, TelegramBotClient client)
        {
            _context = context;
            _client = client;
            Name = "/interval";
        }

        public string Name { get; }
        
        public async void OnHandler(MessageEventArgs e)
        {
           var user = await _context.Users.FirstOrDefaultAsync(userContext =>
                userContext.Username == e.Message.From.Username);

            var setting = await _context.Settings.FirstOrDefaultAsync(s => s.LearningUserId == user.LearningUserId);
            var interval = e.Message.Text.Remove(0, Name.Length).Split(':').Select(int.Parse).ToArray();
            if (user != null)
                setting.Interval = new TimeSpan(interval[0], interval[1], 0);

            await _context.SaveChangesAsync();
            
            await _client.SendTextMessageAsync(e.Message.Chat.Id, $"Вы успешно установили время обучения каждые {interval[0]} часов и {interval[1]} минут");
        }
    }
}