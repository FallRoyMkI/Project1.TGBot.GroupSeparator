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
            _projectCore.Teachers[0] = _teacher;
            _projectCore.SaveAll();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long id = GetUserId(update);
            string tmpUserName = GetUserName(update);

            Console.WriteLine("Есть контакт!");
            Console.WriteLine();

            ActiveUsersAuthorization(update, id, tmpUserName);

            MessageModel message = _tmpUser[id].GetAnswer(update);

            if (message.Text != "STUBMESSAGE!!!")
            {
                if (!message.IsNeedToBeEdited)
                {
                    await _bot.SendTextMessageAsync(id, message.Text, replyMarkup: message.Keyboard);
                }
                else
                {
                    await _bot.EditMessageTextAsync(id, update.CallbackQuery.Message.MessageId, message.Text, replyMarkup: message.Keyboard);
                }
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Что-то пошло не так {exception.Message}");
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
                    throw new Exception("В ИЗВЕСТНОЕ ВРЕМЯ, ИЗВЕСТНАЯ ЛИЧНОСТЬ, ОТПРАВИЛА НЕИЗВЕСТНЫЙ ТИП АПДЕЙТА");
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
                    throw new Exception("В ИЗВЕСТНОЕ ВРЕМЯ, ИЗВЕСТНАЯ ЛИЧНОСТЬ, ОТПРАВИЛА НЕИЗВЕСТНЫЙ ТИП АПДЕЙТА");
            }
        }

        private void ActiveUsersAuthorization(Update update, long id, string tmpUserName)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    if (_projectCore.Teachers.Contains(_projectCore.Teachers.Find(x => x.Id == id)))
                    {
                        Console.WriteLine($"Авторизован преподаватель: {id} {tmpUserName}");
                        if (!_tmpUser.IsContais(id))
                        {
                            _tmpUser.AddTeacher(id);
                        }
                    }
                    else if (!_projectCore.Students.Contains(_projectCore.Students.Find(x => x.Id == id)))
                    {
                        Console.WriteLine($"Авторизован студент c добавлением в Storage: {tmpUserName}. Id: {id}");
                        if (update.Message.Chat.Username != null)
                        {
                            _teacher.CreateNewStudent(id, tmpUserName, update.Message.Chat.Username);
                        }
                        else
                        {
                            _teacher.CreateNewStudent(id, tmpUserName, "NOINFO");
                        }

                        _tmpUser.AddUsers(id);
                        _bot.SendTextMessageAsync(id, "Вы авторизованы как студент");
                    }
                    else
                    {
                        Console.WriteLine($"Авторизован студент: {tmpUserName}. Id: {id}");
                        if (!_tmpUser.IsContais(id))
                        {
                            _tmpUser.AddUsers(id);
                        }
                    }
                    break;
            }
            
        }



        public static void DeleteOldReplyMarkupForCallbackQuery(Update update)
        {
           _bot.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: null);
        }
        public static void DeleteOldReplyForMessage(Update update)
        {
            _bot.EditMessageTextAsync(update.Message.Chat.Id, update.Message.MessageId, update.Message.Text, replyMarkup: null);
        }
        public static void DeleteActualMessage(Update update)
        {
            _bot.DeleteMessageAsync(update.Message.Chat.Id, update.Message.MessageId);
        }
        public static void DeleteOldMessageByCallbackQuery(Update update)
        {
            _bot.DeleteMessageAsync(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Message.MessageId);
        }
    }
}