using System.IO;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramJapaneseLearningBot.DBContext;

namespace TelegramJapaneseLearningBot
{
    public class LearningBot
    {
        private readonly Context _context;
        private TelegramBotClient botClient;

        public LearningBot(Context context, IConfiguration config)
        {
            _context = context;
        }

        public void Start()
        {
            var token = Directory.GetCurrentDirectory();
            botClient = new TelegramBotClient("YOUR_ACCESS_TOKEN_HERE");
        }
    }
}