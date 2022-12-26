using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Microsoft.VisualBasic;
using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates;

public class StateInitialiseTeamBuilder : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    private List<Student> _list = new List<Student>();

    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.WrongInitialization;
        _list.AddRange((_projectCore.Groups.Find(x => x.Id == controller.ActualGroupId).StudentsInGroup).
            FindAll(x => x.Status == StatusType.PassedSurvey));
        if (_list.Count == 0)
        {
            controller.State = new StateIntoGroupMenu();
            result = TeacherMessageGenerator.NotEnoughStudents;
        }
        else
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    switch (update.Message.Text)
                    {
                        case "ПЕРЕСОБРАТЬ":
                            TeamBuilder teamBuilder = new TeamBuilder(_projectCore.Groups.Find(x => x.Id == controller.ActualGroupId),
                                controller.ActualTeams);
                            teamBuilder.TeamBuild();
                            string textMessage = StringBuilder(teamBuilder.TeamList);
                            controller.ActualTeamList = teamBuilder.TeamList;
                            controller.State = new StateWaitForConfirmation();
                            result = TeacherMessageGenerator.StringToBot(textMessage);
                            break;

                        default:
                            if (ArrayCreator(update).Length != 0)
                            {
                                TeamBuilder tb = new TeamBuilder(_projectCore.Groups.Find(x => x.Id == controller.ActualGroupId),
                                    ArrayCreator(update));
                                tb.TeamBuild();
                                string text = StringBuilder(tb.TeamList);
                                controller.ActualTeams = ArrayCreator(update);
                                controller.ActualTeamList = tb.TeamList;
                                controller.State = new StateWaitForConfirmation();
                                result = TeacherMessageGenerator.StringToBot(text);
                            }

                            break;
                    }

                    break;
            }
        }
        

        return result;
    }

    private int[] ArrayCreator(Update update)
    {
        string[] text = update.Message.Text.Split(" ").ToArray();
        List<int> tmp = new List<int>();
        foreach (var element in text)
        {
            if (IsDigital(element))
            {
                tmp.Add(Convert.ToInt32(element));
            }
        }
        int[] result = tmp.ToArray();
        return result;
    }
    private bool IsDigital(string text)
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

    private string StringBuilder(List<List<Student>> list)
    {
        string result = "";
        foreach (var team in list)
        {
            result += "Сформированная команда:" + Environment.NewLine;
            foreach (var student in team)
            {
                result += $"{student.Id} {student.PersonName}" + Environment.NewLine;
            }
        }
        result += "Если Вас не устраивают собранные команды введите \"ПЕРЕСОБРАТЬ\"" + Environment.NewLine;
        result += "Если Вас всё устраивает  введите \"ПОДТВЕРДИТЬ\"" + Environment.NewLine;
        result += "Если хотите увидеть меню взаимодействия с группами введите \"НАЗАД\"" + Environment.NewLine;
        return result;
    }
}
