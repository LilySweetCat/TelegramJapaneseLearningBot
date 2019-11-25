using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.AspNetCore.Mvc;

namespace TelegramJapaneseLearningBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly LearningBot _bot;

        public TelegramController(LearningBot bot)
        {
            _bot = bot;
        }

        public string StartBot()
        {
            _bot.Start();
            return "Bot started";
        }
    }
}