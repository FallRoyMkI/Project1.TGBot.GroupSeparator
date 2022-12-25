using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States
{
    public class StateIntoGroup : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.GroupMenu;

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "goToQuestionnaire":
                            controller.State = new StateFillingQuestionare();
                            result = StudentMessageGenerator.QuestionAboutFreeDays;
                            break;
                        case "groupMembers":
                            // добавить вывод инфы о студентах в группе
                            break;
                        case "status":
                            // добавить вывод инфы о студенте
                            break;
                    }
                    break;

                    break;
            }

            return result;
        }
    }
}
