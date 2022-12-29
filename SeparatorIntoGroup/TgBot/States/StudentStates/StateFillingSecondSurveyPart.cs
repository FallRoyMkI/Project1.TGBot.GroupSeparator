using SeparatorIntoGroup.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SeparatorIntoGroup.TgBot.States.StudentStates;

public class StateFillingSecondSurveyPart : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    private List<string> _str = new List<string>();

    public MessageModel HandleUpdate(Update update, MemberController controller)
    {

        MessageModel result = StudentMessageGenerator.SelectWishStudents;
        switch (update.Type)
        {
            case UpdateType.Message:
                switch (_str.Count)
                {
                    case 0:
                        if (update.Message.Text.ToUpper() != "ДАЛЕЕ")
                        {
                            List<string> students = update.Message.Text.Split(" ").ToList();
                            if (students.Count > 0)
                            {
                                FillWishStudentInQuestionare(update, students);
                            }
                            _str.Add(update.Message.Text);
                        }
                        else
                        {
                            
                            _str.Add("+");
                        }
                        result = StudentMessageGenerator.SelectNotWishStudents;
                        break;

                    case 1:
                        if (update.Message.Text.ToUpper() != "ДАЛЕЕ")
                        {
                            List<string> students = update.Message.Text.Split(" ").ToList();
                            if (students.Count > 0)
                            {
                                FillNotWishStudentInQuestionare(update, students);
                            }
                            _str.Add(update.Message.Text);
                        }
                        else
                        {
                            
                            _str.Add("+");
                        }
                        ChangeStudentStatusToPassSurvey(update);
                        controller.State = new StateIntoGroup();
                        result = StudentMessageGenerator.GroupMenu;

                        break;
                }
                break;
            default:
                BotManager.DeleteActualMessage(update);
                result = StudentMessageGenerator.StubMessage;
                break;

        }

        return result;
    }

    private void FillWishStudentInQuestionare(Update update, List<string> list)
    {
        _projectCore.Students.Find(x => x.Id == update.Message.Chat.Id).AnswersToQuestionnaire.PreferredTeammates = list;
        _projectCore.SaveAll();
    }

    private void FillNotWishStudentInQuestionare(Update update, List<string> list)
    {
        _projectCore.Students.Find(x => x.Id == update.Message.Chat.Id).AnswersToQuestionnaire.NotPreferredTeammates = list;
        _projectCore.SaveAll();
    }

    private void ChangeStudentStatusToPassSurvey(Update update)
    {
        long groupId = _projectCore.Students.Find(x => x.Id == update.Message.Chat.Id).GroupId;

        _projectCore.Groups.Find(x => x.Id == groupId).StudentsInGroup
            .Find(x => x.Id == update.Message.Chat.Id).Status = StatusType.PassedSurvey;
        _projectCore.SaveAll();
    }
}