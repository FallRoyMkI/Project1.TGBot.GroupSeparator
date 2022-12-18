using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class BotManager
{
    public void Ask(Student student)
    {
        Questionnaire question = new Questionnaire();
        question.QuestionAboutWishStudents(new List<Student>());
        question.QuestionAboutNotWishStudents(new List<Student>());
        question.QuestionAboutFreeTime(TimeDictionaryKeys.Monday,new List<TimeDictionaryValues>());
        student.AnswersToQuestionnaire = question;   
    }
}