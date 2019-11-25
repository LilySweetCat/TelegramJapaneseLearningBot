using System.Net;
using System.Reflection;
using Autofac;
using Autofac.Core;
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
            
            builder.RegisterType<TelegramBotClient>().SingleInstance().AsSelf().UsingConstructor(typeof(string), typeof(IWebProxy))
                .WithParameters(new []
                {
                    new ResolvedParameter(
                        (pi, ctx) => pi.Name == "token",
                        (pi, ctx) => ctx.Resolve<IConfiguration>().GetValue<string>("Telegram:Token")),
                    new ResolvedParameter(
                        (pi, ctx) => pi.Name == "webProxy",
                        (pi, ctx) =>
                        {
                            var configuration = ctx.Resolve<IConfiguration>();
                            var proxy = new WebProxy(configuration.GetValue<string>("Proxy:Host"),
                                configuration.GetValue<int>("Proxy:Port"))
                            {
                                Credentials = new NetworkCredential("Anonymous", "Password")
                            };
                            return proxy;
                        })
                });

            builder.RegisterType<LearningBot>().InstancePerLifetimeScope().AsSelf();

            builder.RegisterType<CallbackProcessor>().InstancePerLifetimeScope().AsSelf();
            
            builder.RegisterType<MessageProcessor>().InstancePerLifetimeScope().AsSelf();
        }
    }
}