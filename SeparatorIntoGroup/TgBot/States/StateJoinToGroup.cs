using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup
{
    public class StateJoinToGroup : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.WrongGroupAvtorizationKey;

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (СheckTypeOfText(update.Message.Text))
                    {
                        long groupKey = Convert.ToInt64(update.Message.Text);
                        if (_projectCore.Groups.Contains(_projectCore.Groups.Find(x => x.Id == groupKey)))
                        {
                            result = StudentMessageGenerator.GroupMenu;
                        }
                    }
                    break;
            }

            return result;
        }
        private bool СheckTypeOfText(string text) 
        {
            for(int i = 0; i < text.Length; i++) 
            {
                if (!Char.IsDigit(text[i])) 
                {
                    return false;
                }
            }
            return true;
        }
    }
}
