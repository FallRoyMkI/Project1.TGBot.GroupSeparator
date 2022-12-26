using SeparatorIntoGroup.Options;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SeparatorIntoGroup.TgBot.States;

public class StateFillingStudentsQuestionary : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    private List<string> _str = new List<string>();

    public MessageModel HandleUpdate(Update update, MemberController controller)
    {

        MessageModel result = StudentMessageGenerator.QuestionAboutWishStudents;
        switch (update.Type)
        {
            case UpdateType.Message:
                switch (_str.Count)
                {
                    case 0:
                        List<string> students = update.Message.Text.Split(" ").ToList();
                        if (students.Count > 0)
                        {
                            FillWishStudentInQuestionare(update, students);
                        }
                        result = StudentMessageGenerator.QuestionAboutNotWishStudents;
                        students.Clear();
                        _str.Add(update.Message.Text);
                        break;

                    case 1:
                        students = update.Message.Text.Split(" ").ToList();
                        if (students.Count > 0)
                        {
                            FillNotWishStudentInQuestionare(update, students);
                        }
                        controller.State = new StateIntoGroup();
                        result = StudentMessageGenerator.GroupMenu;
                        _projectCore.Students.Find(x => x.Id == update.Message.Chat.Id).Status = StatusType.PassedSurvey;
                        _projectCore.SaveAll();
                        break;
                }
                break;
        }

        return result;
    }

    private void FillWishStudentInQuestionare(Update update, List<string> list)
    {
        ProjectCore pc = ProjectCore.GetProjectCore();
        pc.Students.Find(x => x.Id == update.Message.Chat.Id).AnswersToQuestionnaire.WishStudents = list;
        pc.SaveAll();
    }

    private void FillNotWishStudentInQuestionare(Update update, List<string> list)
    {
        ProjectCore pc = ProjectCore.GetProjectCore();

        pc.Students.Find(x => x.Id == update.Message.Chat.Id).AnswersToQuestionnaire.NotWishStudents = list;
        pc.SaveAll();
    }
}