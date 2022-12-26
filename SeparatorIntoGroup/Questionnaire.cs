using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Questionnaire
{
    public Dictionary<TimeDictionaryKeys, List<TimeDictionaryValues>> StudentFreeTime { get; set; }
    public List<string> WishStudents { get; set; }
    public List<string> NotWishStudents { get; set; }

    public Questionnaire()
    {
        WishStudents = new List<string>();
        NotWishStudents = new List<string>();
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
   
   

    public override bool Equals(object? obj)
    {
        if (obj is Questionnaire)
        {
            List<string> wishStudents = ((Questionnaire)obj).WishStudents;
            if (WishStudents.Count != wishStudents.Count)
            {
                return false;
            }

            for (int i = 0; i < WishStudents.Count; i++)
            {
                if (!wishStudents[i].Equals(WishStudents[i]))
                {
                    return false;
                }
            }
        }
        if (obj is Questionnaire)
        {
            List<string> notWishStudents = ((Questionnaire)obj).NotWishStudents;
            if (NotWishStudents.Count != notWishStudents.Count)
            {
                return false;
            }

            for (int i = 0; i < NotWishStudents.Count; i++)
            {
                if (!notWishStudents[i].Equals(NotWishStudents[i]))
                {
                    return false;
                }
            }
        }
        if (obj is Questionnaire)
        {
            Dictionary<TimeDictionaryKeys, List<TimeDictionaryValues>> studentFreeTime  = ((Questionnaire)obj).StudentFreeTime;
            var listone = studentFreeTime.ToList();
            var listtwo = StudentFreeTime.ToList();
            if (listtwo.Count != listone.Count)
            {
                return false;
            }

            for (int i = 0; i < listtwo.Count; i++)
            {
                if (!listone[i].Equals(listtwo[i]))
                {
                    return false;
                }
            }
        }

        return true;
    }
}