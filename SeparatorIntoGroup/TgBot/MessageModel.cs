using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace SeparatorIntoGroup
{
    public class MessageModel
    {
        public string Text { get; set; }
        public InlineKeyboardMarkup Keyboard { get; set; }

        public bool IsNeedToBeEdited { get; set; }
    }
}
