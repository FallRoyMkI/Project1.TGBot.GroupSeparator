using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates;

public class StateWaitForConfirmation : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();

    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.ConfirmationNotGet;

        switch (update.Type)
        {
            case UpdateType.CallbackQuery:
                switch (update.CallbackQuery.Data)
                {
                    case "rebuild":
                        BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                        controller.State = new StateInitialiseTeamBuilder();
                        result = TeacherMessageGenerator.Rebuilder;

                        break;
                    case "confirm":
                        BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                        int id = Convert.ToInt32(controller.CurrentGroupId) + 10000;
                        if (_projectCore.Groups.Find(x => x.Id == controller.CurrentGroupId).TeamsInGroup.Count != 0)
                        {
                            id = _projectCore.Groups.Find(x => x.Id == controller.CurrentGroupId).
                                TeamsInGroup[_projectCore.Groups.Find(x => x.Id == controller.CurrentGroupId).TeamsInGroup.Count - 1].Id + 1;
                        }
                        foreach (var team in controller.PreliminaryTeamsList)
                        {
                            _projectCore.Teachers[0].CreateNewTeam(_projectCore.Groups.Find(x => x.Id == controller.CurrentGroupId),
                                id, $"team№ {id}");
                            foreach (var student in team)
                            {
                                _projectCore.Teachers[0].AddStudentToTeam(_projectCore.Groups.Find(x => x.Id == controller.CurrentGroupId),
                                    _projectCore.Groups.Find(x => x.Id == controller.CurrentGroupId).TeamsInGroup.Find(x => x.Id == id), student);
                            }

                        }
                        controller.State = new StateIntoGroupMenu();
                        result = TeacherMessageGenerator.GroupMenu;
                        break;
                    case "back":
                        BotManager.DeleteOldReplyMarkupForCallbackQuery(update);
                        controller.State = new StateIntoGroupMenu();
                        result = TeacherMessageGenerator.GroupMenu;
                        break;
                }

                break;
            default:
                if (update.Message.Text != null)
                {
                    BotManager.DeleteActualMessage(update);
                    result = StudentMessageGenerator.StubMessage;
                }

                break;
        }

        return result;
    }
}