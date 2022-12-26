using SeparatorIntoGroup;
using Telegram.Bot.Types.ReplyMarkups;



ProjectCore pc = ProjectCore.GetProjectCore();
pc.LoadAll();

BotManager bot = new BotManager();

Console.Read();