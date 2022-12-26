using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates;

public class StateConfirmationDeletingGroup : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();

    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.ConfirmationNotGet;

        switch (update.Type)
        {
            case UpdateType.Message:
                if ("ДА Я УВЕРЕН" == update.Message.Text.ToUpper())
                {
                    _projectCore.Teachers[0]
                        .DeleteGroup(_projectCore.Groups.Find(x => x.Id == controller.ActualGroupId));
                    controller.State = new StartTeacherState();
                    result = TeacherMessageGenerator.SuccessfulGroupDeleting;
                }
                else
                {
                    controller.State = new StateIntoGroupMenu();
                }

                break;
        }

        return result;
    }
}