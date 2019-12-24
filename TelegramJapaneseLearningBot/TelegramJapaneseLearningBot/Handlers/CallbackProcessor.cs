using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class CallbackProcessor
    {
        private readonly IEnumerable<IHandler<CallbackQueryEventArgs>> _handlers;

        public CallbackProcessor(IEnumerable<IHandler<CallbackQueryEventArgs>> handlers) => _handlers = handlers;

        public void Handle(CallbackQueryEventArgs e)
        {
            var handler = _handlers.FirstOrDefault(h => h.GetType().Name == e.CallbackQuery.Data);
            handler?.OnHandler(e);
        }
    }
}