using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States.StudentStates
{
    public class StateJoinToGroup : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.WrongGroupAuthorizationKey;

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (СheckTypeOfText(update.Message.Text))
                    {
                        long groupKey = Convert.ToInt64(update.Message.Text);
                        if (_projectCore.Groups.Contains(_projectCore.Groups.Find(x => x.Id == groupKey)))
                        {
                            controller.State = new StateIntoGroup();
                            result = StudentMessageGenerator.GroupMenu;
                            _projectCore.Teachers[0].AddStudentToGroup(_projectCore.Groups.Find(x => x.Id == groupKey), 
                                _projectCore.Students.Find(x => x.Id == update.Message.Chat.Id));
                            _projectCore.SaveAll();
                        }
                    }
                    break;
            }

            return result;
        }
        private bool СheckTypeOfText(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsDigit(text[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
