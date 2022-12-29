using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Questionnaire
{
    public Dictionary<TimeDictionaryKeys, List<TimeDictionaryValues>> StudentFreeTime { get; set; }
    public List<string> PreferredTeammates { get; set; }
    public List<string> NotPreferredTeammates { get; set; }

    public Questionnaire()
    {
        PreferredTeammates = new List<string>();
        NotPreferredTeammates = new List<string>();
        StudentFreeTime = new Dictionary<TimeDictionaryKeys, List<TimeDictionaryValues>>()
        {
            { TimeDictionaryKeys.Понедельник, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Вторник, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Среда, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Четверг, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Пятница, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Суббота, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Воскресенье, new List<TimeDictionaryValues>() }
        };
    }

    public void QuestionAboutFreeTime(List<TimeDictionaryKeys>  keys, List<TimeDictionaryValues> values)
    {
        StudentFreeTime = new Dictionary<TimeDictionaryKeys, List<TimeDictionaryValues>>()
        {
            { TimeDictionaryKeys.Понедельник, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Вторник, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Среда, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Четверг, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Пятница, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Суббота, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Воскресенье, new List<TimeDictionaryValues>() }
        };
        foreach (var key in keys)
        {
            StudentFreeTime[key].AddRange(values);
        }
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is Questionnaire)
        {
            List<string> wishStudents = ((Questionnaire)obj).PreferredTeammates;
            if (PreferredTeammates.Count != wishStudents.Count)
            {
                return false;
            }

            for (int i = 0; i < PreferredTeammates.Count; i++)
            {
                if (!wishStudents[i].Equals(PreferredTeammates[i]))
                {
                    return false;
                }
            }
        }
        if (obj is Questionnaire)
        {
            List<string> notWishStudents = ((Questionnaire)obj).NotPreferredTeammates;
            if (NotPreferredTeammates.Count != notWishStudents.Count)
            {
                return false;
            }

            for (int i = 0; i < NotPreferredTeammates.Count; i++)
            {
                if (!notWishStudents[i].Equals(NotPreferredTeammates[i]))
                {
                    return false;
                }
            }
        }
        if (obj is Questionnaire)
        {
            var listOne = StudentFreeTime.ToList();
            var listTwo = (((Questionnaire)obj).StudentFreeTime).ToList();
            if (listOne.Count != listTwo.Count)
            {
                return false;
            }
        }
        return true;
    }
}