using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;
using TelegramJapaneseLearningBot.Models;

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
        
        public void OnHandler(MessageEventArgs e)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == e.Message.From.Id);
            var interval = e.Message.Text.Remove(0, Name.Length).Split(':').Select(int.Parse).ToArray();
            if (user != null)
                user.UserSettings.Interval = new TimeSpan(interval[0], interval[1], 0);
            
            _client.SendTextMessageAsync(e.Message.Chat, $"Вы успешно установили время обучения каждые {interval[0]} часов и {interval[1]} минут");
        }
    }
}