using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using SeparatorIntoGroup.Options;
using Telegram.Bot;

namespace SeparatorIntoGroup.TgBot.States.StudentStates
{
    public class StartStudentState : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.StartMenu;

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    switch (update.CallbackQuery.Data)
                    {
                        case "joinGroup":
                            BotManager.DeleteOldReplyMarkup(update);
                            controller.State = new StateJoinToGroup();
                            result = StudentMessageGenerator.GroupAuthorization;
                            break;

                        case "status":
                            BotManager.DeleteOldReplyMarkup(update);
                            StatusType status = _projectCore.Students.Find(x => x.Id == update.CallbackQuery.From.Id).Status;
                            result = StudentMessageGenerator.StudentStatusMessage(update, status);
                            break;
                    }
                    break;
            }
            return result;
        }
    }
}
