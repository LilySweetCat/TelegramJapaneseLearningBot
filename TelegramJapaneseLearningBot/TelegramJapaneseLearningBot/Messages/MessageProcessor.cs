using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Args;

namespace TelegramJapaneseLearningBot.Messages
{
    public class MessageProcessor
    {
        private readonly IEnumerable<IHandler<MessageEventArgs>> _handlers;

        public MessageProcessor(IEnumerable<IHandler<MessageEventArgs>> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(MessageEventArgs args)
        {
            var handler = _handlers.FirstOrDefault(h => h.GetType().Name == args.Message.Text) ?? _handlers.FirstOrDefault(h => h.GetType() == typeof(UndefinedMessageHandler));
            handler?.OnHandler(args);
        }
    }
}