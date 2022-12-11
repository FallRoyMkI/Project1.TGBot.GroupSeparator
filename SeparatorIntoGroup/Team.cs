using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Team
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public List<Student> StudentsInTeam { get; set; }

    public Team(int id, string name)
    {
        Id = id;
        TeamName = name;
        StudentsInTeam = new List<Student>();
    }

    public void AddStudentToTeam(Student student)
    {
        StudentsInTeam.Add(student);
        student.Status = StatusType.InTeam;
    }

    public void RemoveStudentFromTeam(Student student)
    {
        StudentsInTeam.Remove(student);
        student.Status = StatusType.PassedSurvey;
    }

    public void RemoveAllStudentsFromTeam()
    {
        foreach (var student in StudentsInTeam)
        {
            student.Status = StatusType.PassedSurvey;
        }
        StudentsInTeam.Clear();
    }
}