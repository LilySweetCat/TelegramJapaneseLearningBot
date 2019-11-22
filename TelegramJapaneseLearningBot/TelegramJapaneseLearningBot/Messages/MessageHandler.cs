using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;

namespace TelegramJapaneseLearningBot.Messages
{
    public class MessageHandler
    {
        private readonly IEnumerable<IHandler<MessageEventArgs>> _handlers;

        public MessageHandler(IEnumerable<IHandler<MessageEventArgs>> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(MessageEventArgs args)
        {
            var handler = _handlers.FirstOrDefault(h => args.Message.Text.Contains(h.Name));
            handler?.OnHandler(args);
        }
    }
}