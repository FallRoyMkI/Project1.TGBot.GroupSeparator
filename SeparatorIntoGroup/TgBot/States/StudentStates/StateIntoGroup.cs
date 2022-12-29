using SeparatorIntoGroup.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SeparatorIntoGroup.TgBot.States.StudentStates
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
                        case "takeASurvey":
                            BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                            controller.State = new StateFillingFirstSurveyPart();
                            result = StudentMessageGenerator.QuestionAboutFreeDays;
                            break;

                        case "groupMembers":
                            BotManager.DeleteOldMessageByCallbackQuery(update);
                            string groupMembers = StringBuilder(update);
                            result = StudentMessageGenerator.GroupMembers(update, groupMembers);
                            break;

                        case "status":
                            BotManager.DeleteOldMessageByCallbackQuery(update);
                            StatusType status = _projectCore.Students.Find(x => x.Id == update.CallbackQuery.From.Id).Status;
                            result = StudentMessageGenerator.StudentStatusMessage(update, status);
                            break;
                    }

                    break;
                default:
                    if (update.Message.Text.ToUpper() != "НА ДАННЫЙ МОМЕНТ ВЫ НАХОДИТЕСЬ В ГРУППОВОМ МЕНЮ:)" && update.Message.Text.ToUpper() != "/START")
                    {
                        BotManager.DeleteActualMessage(update);
                        result = StudentMessageGenerator.StubMessage;
                    }
                    
                    break;
            }
            return result;
        }

        public string StringBuilder(Update update)
        {
            List<Student> students = new List<Student>();
            long groupId = _projectCore.Students.Find(x => x.Id == update.CallbackQuery.Message.Chat.Id).GroupId;

            students.AddRange(_projectCore.Students.FindAll(x => x.GroupId == groupId));
            students.Remove(_projectCore.Students.Find(x => x.Id == update.CallbackQuery.Message.Chat.Id));
            string groupMembers = "";
            if (students.Count > 0)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    groupMembers += $"{i + 1} {students[i].PersonName} {students[i].AccountName}" + Environment.NewLine;
                }
            }
            else
            {
                groupMembers = "На данный момент в группе находитесь только вы";
            }

            return groupMembers;
        }
    }
}
