using System;
using Telegram.Bot.Args;

namespace TelegramJapaneseLearningBot
{
    public interface IHandler<T>
    {
        string Name { get; }

        void OnHandler(T e);
    }
}