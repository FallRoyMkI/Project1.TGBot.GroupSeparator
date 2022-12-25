using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace SeparatorIntoGroup.TgBot
{
    internal class TeacherMessageGenerator
    {
        public static MessageModel StartMenu
        {
            get
            {
                return new MessageModel()
                {
                    Text = "Привет препод",
                    Keyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton("ййй") { CallbackData = "asasas" },
                                new InlineKeyboardButton("ввв") { CallbackData = "asasas" }
                            },
                        })
                };
            }
        }
    }
}
