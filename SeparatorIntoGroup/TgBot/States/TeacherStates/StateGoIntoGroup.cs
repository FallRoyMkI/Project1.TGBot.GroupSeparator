using SeparatorIntoGroup.TgBot.States.StudentStates;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates;

public class StateGoIntoGroup : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.GetWrongGroupId;

        switch (update.Type)
        {
            case UpdateType.Message:
                if (СheckTypeOfText(update.Message.Text))
                {
                    long groupKey = Convert.ToInt64(update.Message.Text);
                    if (_projectCore.Groups.Contains(_projectCore.Groups.Find(x => x.Id == groupKey)))
                    {
                        controller.State = new StateIntoGroupMenu();
                        controller.CurrentGroupId = groupKey;
                        result = TeacherMessageGenerator.GroupMenu;
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
