using SeparatorIntoGroup.Options;
using SeparatorIntoGroup.TgBot.States.StudentStates;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates
{
    public class StateCreateGroup : IState
    {
        private ProjectCore _projectCore = ProjectCore.GetProjectCore();
        private List<string> _str = new List<string>();
        private long _groupId;

        public MessageModel HandleUpdate(Update update, MemberController controller)
        {
            MessageModel result = TeacherMessageGenerator.WrongGroupId;

            switch (update.Type)
            {
                case UpdateType.Message:
                    switch (_str.Count)
                    {
                        case 0:
                            if (СheckTypeOfText(update.Message.Text))
                            {
                                _groupId = Convert.ToInt64(update.Message.Text);
                                if (!_projectCore.Groups.Contains(_projectCore.Groups.Find(x => x.Id == _groupId)))
                                {
                                    _str.Add(update.Message.Text);
                                    result = TeacherMessageGenerator.CreateGroupName;
                                }
                            }
                            break;

                        case 1:
                            string name = update.Message.Text;
                            _projectCore.Teachers[0].CreateNewGroup(_groupId, name);
                            _projectCore.SaveAll();
                            controller.State = new StartTeacherState();
                            result = TeacherMessageGenerator.StartMenu;
                            break;
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
}