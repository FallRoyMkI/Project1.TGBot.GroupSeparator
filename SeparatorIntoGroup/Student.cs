using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Student: AbstractPersons
{
    public Group? Group { get; set; }
    public Team? Team { get; set; }
    public Questionnaire? AnswersToQuestionnaire { get; set; }

    public Student(int id, string name, string userName)
    {
        Id = id;
        PersonName = name;
        AccountName = userName;
        Status = StatusType.NotInGroup;
        Group = null;
        Team = null;
        AnswersToQuestionnaire = null;
    }
}