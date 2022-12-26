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
            case UpdateType.Message:
                if ("ПОДТВЕРДИТЬ" == update.Message.Text.ToUpper())
                {
                    int id = Convert.ToInt32(controller.ActualGroupId) + 10000;
                    if (_projectCore.Groups.Find(x => x.Id == controller.ActualGroupId).TeamsInGroup.Count != 0)
                    {
                        id = _projectCore.Groups.Find(x => x.Id == controller.ActualGroupId).
                            TeamsInGroup[_projectCore.Groups.Find(x => x.Id == controller.ActualGroupId).TeamsInGroup.Count-1].Id;
                    }
                    foreach (var team in controller.ActualTeamList)
                    {
                        _projectCore.Teachers[0].CreateNewTeamInGroup(_projectCore.Groups.Find(x=> x.Id == controller.ActualGroupId),
                            id,$"team№ {id}");
                        foreach (var student in team)
                        {
                            _projectCore.Teachers[0].AddStudentToTeam(_projectCore.Groups.Find(x => x.Id == controller.ActualGroupId),
                                _projectCore.Groups.Find(x => x.Id == controller.ActualGroupId).TeamsInGroup.Find(x=> x.Id == id), student);
                        }
                    }
                    controller.State = new StateIntoGroupMenu();
                    result = TeacherMessageGenerator.GroupMenu;
                }
                if ("ПЕРЕСОБРАТЬ" == update.Message.Text.ToUpper())
                {
                    controller.State = new StateInitialiseTeamBuilder();
                    result = TeacherMessageGenerator.Rebuilder;
                }
                if ("НАЗАД" == update.Message.Text.ToUpper())
                {
                    controller.State = new StateIntoGroupMenu();
                    result = TeacherMessageGenerator.GroupMenu;
                }
                break;
        }

        return result;
    }
}