using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SeparatorIntoGroup
{
    public class BotManager
    {
        private static ITelegramBotClient _bot;
        //private TmpUsersController _users;
        private ProjectCore _projectCore;
        private Student _studen;
        private Teacher _teacher;

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
                AllowedUpdates = new UpdateType[] { UpdateType.CallbackQuery }, //add UpdateType.Message,
                Limit = (20),
                ThrowPendingUpdates = true // delete all pending updates at startup
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
            long id = GetUserId(update);
            string tmpUserName = GetUserName(update);

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Есть контакт!");
            Console.WriteLine($"FirstName: {tmpUserName}");
            Console.WriteLine($"Id: {id}");
            Console.WriteLine();
            Console.ResetColor();

            

            if (!_projectCore.Students.Contains(_projectCore.Students.Find(x => x.Id == update.Message.Chat.Id))) 
            {
                _teacher.CreateNewStudent(id, update.Message.Chat.FirstName, update.Message.Chat.Username);
            }
            //if (!_activeUsers.IsContais(id))
            //{
            //    _activeUsers.AddActiveMember(id);
            //}

                //if (update.Message.Text is not null)
                //{
                //    Console.WriteLine(update.Message.Text);
                //    if (update.Message.Text.ToLower() == "/start")
                //    {
                //        if (update.Message.Chat.Id == 522974861)
                //        {
                //            _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Привет {update.Message.Chat.FirstName}!", replyMarkup: GetButtons());
                //        }
                //        else
                //        {
                //            Console.BackgroundColor = ConsoleColor.Red;
                //            Console.WriteLine("unregistered user");
                //            Console.ResetColor();

                //            _bot.SendTextMessageAsync(update.Message.Chat.Id, "Ты кто?");
                //        }
                //    }
                //    else
                //    {
                //        _bot.SendTextMessageAsync(update.Message.Chat.Id, "Для начала работы бота введи: /start");
                //    }
                //}
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine("Что-то пошло не так");
        }

        private long GetUserId(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update.Message.Chat.Id;
                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.From.Id;
                default:
                    throw new Exception();
            }
        }

        private string GetUserName(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update.Message.Chat.FirstName;
                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.From.FirstName;
                default:
                    throw new Exception();
            }
        }

        private IReplyMarkup? GetButtons()
        {
            return new ReplyKeyboardMarkup(new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton("Test1"), new KeyboardButton("Test2") },
                    new List<KeyboardButton> { new KeyboardButton("Test3"), new KeyboardButton("Test4") }
                }
            );
        }
    }
}