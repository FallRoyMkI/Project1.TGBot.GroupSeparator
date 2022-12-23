using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Questionnaire
{
    public Dictionary<TimeDictionaryKeys, List<TimeDictionaryValues>> StudentFreeTime { get; set; }
    public List<Student> WishStudents { get; set; }
    public List<Student> NotWishStudents { get; set; }

    public Questionnaire()
    {
        WishStudents = new List<Student>();
        NotWishStudents = new List<Student>();
        StudentFreeTime = new Dictionary<TimeDictionaryKeys, List<TimeDictionaryValues>>()
        {
            { TimeDictionaryKeys.Monday, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Tuesday, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Wednesday, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Thursday, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Friday, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Saturday, new List<TimeDictionaryValues>() },
            { TimeDictionaryKeys.Sunday, new List<TimeDictionaryValues>() }
        };
    }


    public void QuestionAboutFreeTime(TimeDictionaryKeys key, List<TimeDictionaryValues> values)
    {
        StudentFreeTime[key].AddRange(values);
    }
    public void QuestionAboutWishStudents(List<Student> list)
    {
        WishStudents = list;
    }
    public void QuestionAboutNotWishStudents(List<Student> list)
    {
        NotWishStudents = list;
    }

    public override bool Equals(object? obj)
    {
        return obj is Questionnaire answers &&
               StudentFreeTime == answers.StudentFreeTime &&
               WishStudents == answers.WishStudents &&
               NotWishStudents == answers.NotWishStudents;
    }
}