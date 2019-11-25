using System.Threading.Tasks;
using System.Timers;
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
        private readonly CallbackProcessor _callbackProcessor;
        private readonly MessageProcessor _messageProcessor;
        private readonly Context _context;

        private TelegramBotClient _telegramBotClient;

        /// <summary>
        /// Создает нового бота
        /// </summary>
        /// <param name="context"></param>
        /// <param name="callbackProcessor"></param>
        /// <param name="messageProcessor"></param>
        /// <param name="telegramBotClient"></param>
        public LearningBot(Context context, CallbackProcessor callbackProcessor, MessageProcessor messageProcessor,
            TelegramBotClient telegramBotClient)
        {
            _context = context;
            _callbackProcessor = callbackProcessor;
            _messageProcessor = messageProcessor;
            _telegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Запустить
        /// </summary>
        public async void Start()
        {
            _telegramBotClient.OnMessage += OnMessageReceived;
            _telegramBotClient.OnCallbackQuery += OnCallbackQuery;
            _telegramBotClient.StartReceiving();
            await _telegramBotClient.GetUpdatesAsync();
        }

        private void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            _callbackProcessor.Handle(e);
        }

        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Message.Text))
            {
                _messageProcessor.Handle(e);
            }
        }
    }
}