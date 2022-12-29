using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates;

public class StateIntoGroupMenu : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.GroupMenu;

        switch (update.Type)
        {
            case UpdateType.CallbackQuery:
                switch (update.CallbackQuery.Data)
                {
                    case "studentList":
                        BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                        string groupInfo = StringBuilder(update, controller.CurrentGroupId);
                        result = TeacherMessageGenerator.StringToBot(update,groupInfo);
                        break;

                    case "deleteStudent":
                        BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                        controller.State = new StateDeletingStudent();
                        result = TeacherMessageGenerator.DeleteStudent;
                        break;

                    case "createTeams":
                        BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                        controller.State = new StateInitialiseTeamBuilder();
                        result = TeacherMessageGenerator.CreateTeams;
                        break;

                    case "deleteGroup":
                        BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                        controller.State = new StateConfirmationDeletingGroup();
                        result =TeacherMessageGenerator.ConfirmDeleting;
                        break;
                }
                break;
            case UpdateType.Message:
                BotManager.DeleteOldReplyForMessage(update);
                break;
            
        }
        return result;
    }
    public string StringBuilder(Update update, long actualId)
    {
        string groupInfo = "";
        if (_projectCore.Groups.Contains(_projectCore.Groups.Find(x => x.Id == actualId)))
        {
            groupInfo += $"Список студентов которые числятся в группе:" + Environment.NewLine;
                foreach (var student in _projectCore.Groups.Find(x => x.Id == actualId).StudentsInGroup)
                {
                    groupInfo += $"{student.Id} {student.PersonName} {student.AccountName}" + Environment.NewLine;
                }
        }
        else
        {
            groupInfo = "На данный момент в группе нет ни одного студента";
        }

        return groupInfo;
    }
}