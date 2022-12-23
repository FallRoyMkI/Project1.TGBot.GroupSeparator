using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Student: AbstractPersons
{
    public int GroupId { get; set; }
    public int TeamId { get; set; }
    public Questionnaire? AnswersToQuestionnaire { get; set; }

    public Student(long id, string personName, string accountName)
    {
        Id = id;
        PersonName = personName;
        AccountName = accountName;
        Status = StatusType.NotInGroup;
        GroupId = -1;
        TeamId = -1;
        AnswersToQuestionnaire = null;
    }

    public override bool Equals(object? obj)
    {
        return obj is Student student &&
               Id == student.Id &&
               PersonName == student.PersonName &&
               AccountName == student.AccountName &&
               Status == student.Status &&
               GroupId == student.GroupId &&
               TeamId == student.TeamId &&
               AnswersToQuestionnaire == student.AnswersToQuestionnaire;
    }
}