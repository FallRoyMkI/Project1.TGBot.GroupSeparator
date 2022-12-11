namespace SeparatorIntoGroup;

public class Questionnaire
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<Student> WishStudents { get; set; }
    public List<Student> NotWishStudents { get; set; }
    public Questionnaire()
    {
        WishStudents = new List<Student>();
        NotWishStudents = new List<Student>();
    }
    public void QuestionAboutTime(string startTime, string timeDuration)
    {
        StartTime = DateTime.Parse(startTime);
        EndTime = StartTime.AddHours(Convert.ToInt32(timeDuration));
        //Console.WriteLine($"{StartTime.ToShortTimeString()} - {EndTime.ToShortTimeString()}");
    }

    public void QuestionAboutWishStudents(List<Student> list)
    {
        WishStudents = list;
    }
    public void QuestionAboutNotWishStudents(List<Student> list)
    {
        NotWishStudents = list;
    }
}