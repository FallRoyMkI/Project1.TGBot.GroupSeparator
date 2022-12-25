using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States
{
    internal class StateFillingQuestionare : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        private List<string> _str = new List<string>();
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.QuestionAboutFreeDays;

            switch (update.Type)
            {
                case UpdateType.Message:
                    if (true)
                    {
                        // добавить кнопки
                    }

                    break;
            }

            return result;
        }
    }
}
