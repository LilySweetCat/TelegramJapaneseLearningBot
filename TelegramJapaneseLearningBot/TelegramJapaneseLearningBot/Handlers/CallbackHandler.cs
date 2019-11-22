using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;

namespace TelegramJapaneseLearningBot.Handlers
{
    public class CallbackHandler
    {
        private readonly IEnumerable<IHandler<CallbackQueryEventArgs>> _handlers;

        public CallbackHandler(IEnumerable<IHandler<CallbackQueryEventArgs>> handlers) => _handlers = handlers;

        public void Handle(CallbackQueryEventArgs e)
        {
            var handler = _handlers.FirstOrDefault(h => h.Name == e.CallbackQuery.Id);
            handler?.OnHandler(e);
        }
    }
}