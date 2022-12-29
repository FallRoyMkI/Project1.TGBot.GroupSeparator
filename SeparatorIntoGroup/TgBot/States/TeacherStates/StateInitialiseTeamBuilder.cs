using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Microsoft.VisualBasic;
using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup.TgBot.States.TeacherStates;

public class StateInitialiseTeamBuilder : IState
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();
    private List<Student> _studentsForDistribution = new List<Student>();

    public MessageModel HandleUpdate(Update update, MemberController controller)
    {
        MessageModel result = TeacherMessageGenerator.WrongInitialization;

        _studentsForDistribution = ((_projectCore.Groups.Find(x => x.Id == controller.CurrentGroupId).StudentsInGroup).
            FindAll(x => x.Status == StatusType.PassedSurvey));

        if (_studentsForDistribution.Count == 0)
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
                        //case "ПЕРЕСОБРАТЬ":
                        //    TeamBuilder teamBuilder = new TeamBuilder(_studentsForDistribution, controller.CurrentNumberOfTeamMembers);
                        //    teamBuilder.TeamBuild();
                        //    string textMessage = StringBuilder(teamBuilder.TeamList);
                        //    controller.PreliminaryTeamsList = teamBuilder.TeamList;
                        //    controller.State = new StateWaitForConfirmation();
                        //    result = TeacherMessageGenerator.StringToBot(textMessage);
                        //    break;

                        default:
                            if (ListOfTeamMaxTeamMembersCreator(update).Count != 0 
                                && ListOfTeamMaxTeamMembersCreator(update).Count <= _studentsForDistribution.Count
                                && ListOfTeamMaxTeamMembersCreator(update).Sum() > _studentsForDistribution.Count)
                            {
                                TeamBuilder builder = new TeamBuilder(_studentsForDistribution, ListOfTeamMaxTeamMembersCreator(update));
                                builder.TeamBuild();

                                string text = StringBuilder(builder.TeamList);
                                controller.CurrentNumberOfTeamMembers = ListOfTeamMaxTeamMembersCreator(update);
                                controller.PreliminaryTeamsList = builder.TeamList;
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

    private List<int> ListOfTeamMaxTeamMembersCreator(Update update)
    {
        List<string> text = update.Message.Text.Split(" ").ToList();
        List<int> result = new List<int>();

        foreach (var element in text)
        {
            if (IsDigital(element))
            {
                result.Add(Convert.ToInt32(element));
            }
        }
        return result;
    }
    private bool IsDigital(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (!char.IsDigit(line[i]))
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
