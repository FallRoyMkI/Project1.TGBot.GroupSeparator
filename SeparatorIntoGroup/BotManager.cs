using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Microsoft.VisualBasic;

namespace SeparatorIntoGroup
{
    public class BotManager
    {
        private static ITelegramBotClient _bot;

        public BotManager()
        {
            string token = @"5941068451:AAEAgguiOPtThpnOdg2QH88QlK7iMah22pM";
            _bot = new TelegramBotClient(token);

            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"Запущен бот");
            Console.WriteLine($"id: {_bot.GetMeAsync().Result.Id}");
            Console.WriteLine($"Name: {_bot.GetMeAsync().Result.FirstName}");
            Console.WriteLine();
            Console.ResetColor();

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
                Limit = (20)
            };

            _bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Есть контакт!");
            Console.WriteLine($"FirstName: {update.Message.Chat.FirstName}");
            Console.WriteLine($"Id: {update.Message.Chat.Id}");
            Console.WriteLine();
            Console.ResetColor();

            if (update.Message.Text is not null)
            {
                Console.WriteLine(update.Message.Text);
                if (update.Message.Text.ToLower() == "/start")
                {
                    if (update.Message.Chat.Id == 522974861)
                    {
                        _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Привет {update.Message.Chat.FirstName}!");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("unregistered user");
                        Console.ResetColor();

                        _bot.SendTextMessageAsync(update.Message.Chat.Id, "Ты кто?");
                    }
                }
                else
                {
                    _bot.SendTextMessageAsync(update.Message.Chat.Id, "Для начала работы бота введи: /start");
                }
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

        }
    }
}