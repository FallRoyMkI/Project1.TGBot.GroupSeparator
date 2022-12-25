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
        private ProjectCore _projectCore;
        private Teacher _teacher;
        private TmpUsersController _tmpUser = new TmpUsersController();

        public BotManager()
        {
            string token = @"5941068451:AAHcTxwqCpEagEKEN4eRbC2XwttUvqJHgMs";
            _bot = new TelegramBotClient(token);

            Console.WriteLine("Запущен бот");

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
                //Limit = (1),
                ThrowPendingUpdates = true

            };

            _bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,   
                receiverOptions,
                cancellationToken
            );

            _projectCore = ProjectCore.GetProjectCore();
            _projectCore.LoadAll();
            _teacher = new Teacher(0, "admin", "@admin");
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long id = GetUserId(update);
            string tmpUserName = GetUserName(update);

            Console.WriteLine("Есть контакт!");
            Console.WriteLine();

            UsersAvtorisation(update, id, tmpUserName);

            MessageModel message = _tmpUser[id].GetAnswer(update);

            await _bot.SendTextMessageAsync(id, message.Text, replyMarkup: message.Keyboard);
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

        private void UsersAvtorisation(Update update, long id, string tmpUserName)
        {
            if (_projectCore.Teachers.Contains(_projectCore.Teachers.Find(x => x.Id == id)))
            {
                Console.WriteLine($"авторизован преподаватель: {tmpUserName}");
                if (!_tmpUser.IsContais(id))
                {
                    _tmpUser.AddUsers(id);
                }
            }
            else if (!_projectCore.Students.Contains(_projectCore.Students.Find(x => x.Id == id)))
            {
                Console.WriteLine($"авторизован студент c добавлением в Storage: {tmpUserName}. Id: {id}");
                _teacher.CreateNewStudent(id, tmpUserName, update.Message.Chat.Username);
                _tmpUser.AddUsers(id);
                _bot.SendTextMessageAsync(id, "Вы авторизованы как студент");
            }
            else
            {
                Console.WriteLine($"авторизован студент: {tmpUserName}. Id: {id}");
                if (!_tmpUser.IsContais(id))
                {
                    _tmpUser.AddUsers(id);
                }
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