namespace SeparatorIntoGroup;

public class BotManager
{
    public void Ask(Student student)
    {
        Questionnaire question = new Questionnaire();
        question.QuestionAboutWishStudents(new List<Student>());
        question.QuestionAboutNotWishStudents(new List<Student>());
        question.QuestionAboutTime("18:50", "2");
        student.AnswersToQuestionnaire = question;   
    }
}