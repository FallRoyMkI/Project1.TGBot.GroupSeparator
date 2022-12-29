using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States
{
    public interface IState
    {
        public MessageModel HandleUpdate(Update update, MemberController controller);
    }
}
