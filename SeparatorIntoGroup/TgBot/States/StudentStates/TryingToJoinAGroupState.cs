using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States.StudentStates
{
    public class TryingToJoinAGroupState : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = StudentMessageGenerator.WrongGroupAuthorizationKey;
            if (update.Message.Text != null)
            {
                if (IsMessageDigital(update.Message.Text))
                {
                    long groupKey = Convert.ToInt64(update.Message.Text);

                    if (_projectCore.Groups.Contains(_projectCore.Groups.Find(x => x.Id == groupKey)))
                    {
                        controller.State = new StateIntoGroup();
                        result = StudentMessageGenerator.GroupMenu;
                        _projectCore.Teachers[0].AddStudentToGroup(_projectCore.Groups.Find(x => x.Id == groupKey),
                            _projectCore.Students.Find(x => x.Id == update.Message.Chat.Id));
                    }
                }
            }
            return result;
        }
        private bool IsMessageDigital(string text)
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
}
