using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;
using TelegramJapaneseLearningBot.Handlers;
using TelegramJapaneseLearningBot.Messages;

namespace TelegramJapaneseLearningBot
{
    /// <summary>
    /// Бот обучения языку
    /// </summary>
    public class LearningBot
    {
        private readonly CallbackHandler _callbackHandler;
        private readonly MessageHandler _messageHandler;
        private readonly IConfiguration _config;
        private readonly Context _context;

        private TelegramBotClient _telegramBotClient;

        /// <summary>
        /// Создает нового бота
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config"></param>
        /// <param name="callbackHandler"></param>
        /// <param name="messageHandler"></param>
        /// <param name="telegramBotClient"></param>
        public LearningBot(Context context, IConfiguration config, CallbackHandler callbackHandler, MessageHandler messageHandler,
            TelegramBotClient telegramBotClient)
        {
            _context = context;
            _config = config;
            _callbackHandler = callbackHandler;
            _messageHandler = messageHandler;
            _telegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Запустить
        /// </summary>
        public void Start()
        {
            _telegramBotClient = new TelegramBotClient(_config.GetSection("Telegram").Value);
            _telegramBotClient.OnMessage += OnMessageReceived;
            _telegramBotClient.OnCallbackQuery += OnCallbackQuery;
            _telegramBotClient.StartReceiving();
        }


        private void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            _callbackHandler.Handle(e);
        }

        private Task<bool> CheckUser(int userId)
        {
            return _context.Users.AnyAsync(user => user.UserId == userId);
        }

        private async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Message.Text))
            {
                _messageHandler.Handle(e);
            }
        }
    }
}