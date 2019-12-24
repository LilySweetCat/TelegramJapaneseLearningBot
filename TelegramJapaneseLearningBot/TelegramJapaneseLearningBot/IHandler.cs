using System;
using Telegram.Bot.Args;

namespace TelegramJapaneseLearningBot
{
    public interface IHandler<T>
    {
        void OnHandler(T e);
    }
}