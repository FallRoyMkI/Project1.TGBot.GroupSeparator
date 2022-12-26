using SeparatorIntoGroup;
using SeparatorIntoGroup.Options;
using SeparatorIntoGroup.TgBot.States;
using SeparatorIntoGroup.TgBot.States.TeacherStates;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;


public class StartTeacherState : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.StartMenu;

        switch (update.Type)
        {
            case UpdateType.CallbackQuery:
                switch (update.CallbackQuery.Data)
                {
                    case "createGroup":
                        BotManager.DeleteOldReplyMarkup(update);
                        controller.State = new StateCreateGroup();
                        result = TeacherMessageGenerator.CreateGroupId;
                        break;

                    case "groupList":
                        BotManager.DeleteOldReplyMarkup(update);
                        string groupInfo = StringBuilder(update);
                        result = TeacherMessageGenerator.StringToBot(update, groupInfo);
                        break;

                    case "goIntoGroup":
                        BotManager.DeleteOldReplyMarkup(update);
                        controller.State = new StateGoIntoGroup();
                        result = TeacherMessageGenerator.GetGroupId;
                        break;
                }
                break;
        }
        return result;
    }
    public string StringBuilder(Update update)
    {
        string groupInfo = "";
        if (_projectCore.Groups.Count > 0)
        {
            for (int i = 0; i < _projectCore.Groups.Count; i++)
            {
                groupInfo += $"{i + 1}. Id группы: {_projectCore.Groups[i].Id}. Студенты в группе:" + Environment.NewLine;
                foreach (var student in _projectCore.Groups[i].StudentsInGroup)
                {
                    groupInfo += $"{student.Id} {student.PersonName} {student.AccountName}" + Environment.NewLine;
                }
            }
        }
        else
        {
            groupInfo = "На данный момент ни одной группы не создано";
        }

        return groupInfo;
    }
}