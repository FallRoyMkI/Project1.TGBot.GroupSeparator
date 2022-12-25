using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup
{
    public class MemberController
    {
        public long Id { get; set; }
        public IState State { get; set; }

        public MemberController(long id)
        {
            Id = id;
            State = new StartState();
        }

        public MessageModel GetAnswer(Update update)
        {
            return State.HandleUpdate(update, this);
        }
    }
}
