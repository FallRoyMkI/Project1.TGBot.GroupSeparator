using SeparatorIntoGroup.Options;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates;

public class StateDeletingStudent : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.WrongStudentInfo;

        switch (update.Type)
        {
            case UpdateType.Message:
                if (update.Message.Text.ToLower() != "НАЗАД")
                {
                    if (СheckTypeOfText(update.Message.Text))
                    {
                        long studentId = Convert.ToInt64(update.Message.Text);
                        if (_projectCore.Students.Contains(_projectCore.Students.Find(x => x.Id == studentId)))
                        {
                            Student st = _projectCore.Students.Find(x => x.Id == studentId);
                            _projectCore.Teachers[0].RemoveStudentFromGroup(_projectCore.Groups.Find(x => x.Id == st.GroupId), st);
                            controller.State = new StateIntoGroupMenu();
                            result = TeacherMessageGenerator.GroupMenu;
                        }
                    }
                    else
                    {
                        if (_projectCore.Students.Contains(_projectCore.Students.Find(x => x.AccountName == update.Message.Text)))
                        {
                            Student st = _projectCore.Students.Find(x => x.AccountName == update.Message.Text);
                            _projectCore.Teachers[0].RemoveStudentFromGroup(_projectCore.Groups.Find(x => x.Id == st.GroupId), st);
                            controller.State = new StateIntoGroupMenu();
                            result = TeacherMessageGenerator.GroupMenu;
                        }
                    }
                }
                else
                {
                    controller.State = new StateIntoGroupMenu();
                    result = TeacherMessageGenerator.GroupMenu;
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