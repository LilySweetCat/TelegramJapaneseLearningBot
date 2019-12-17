using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.AspNetCore.Mvc;

namespace TelegramJapaneseLearningBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegramController : ControllerBase
    {
        private static LearningBot _bot;
        private readonly ILifetimeScope _scope;

        public TelegramController(LearningBot bot, ILifetimeScope scope)
        {
            _bot = bot;
            _scope = scope;
        }

        public string StartBot()
        {
            _bot.Start();
            return "Bot started";
        }
    }
}