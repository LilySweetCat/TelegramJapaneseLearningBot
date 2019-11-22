using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramJapaneseLearningBot.DBContext;
using TelegramJapaneseLearningBot.Handlers;
using TelegramJapaneseLearningBot.Messages;
using Module = Autofac.Module;

namespace TelegramJapaneseLearningBot
{
    public class TelegramJapaneseLearningBotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            builder.RegisterType<Context>().InstancePerLifetimeScope().AsSelf();
            
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.BaseType == typeof(IHandler<CallbackQueryEventArgs>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(type => type.BaseType == typeof(IHandler<MessageEventArgs>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<TelegramBotClient>().InstancePerLifetimeScope().AsSelf()
                .WithParameter((pi, c) => pi.Name == "token", (pi, c) =>
                {
                    var configuration = c.Resolve<IConfiguration>().GetValue<string>("Telegram:Token");
                    return configuration;
                });

            builder.RegisterType<LearningBot>().InstancePerLifetimeScope().AsSelf();

            builder.RegisterType<CallbackProcessor>().InstancePerLifetimeScope().AsSelf();
            
            builder.RegisterType<MessageProcessor>().InstancePerLifetimeScope().AsSelf();
        }
    }
}