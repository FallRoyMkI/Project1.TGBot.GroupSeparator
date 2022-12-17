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
        student.Team = this;
    }

    public void RemoveStudentFromTeam(Student student)
    {
        StudentsInTeam.Remove(student);
        student.Status = StatusType.PassedSurvey;
        student.Team = null;
    }

    public void RemoveAllStudentsFromTeam()
    {
        foreach (var student in StudentsInTeam)
        {
            student.Status = StatusType.PassedSurvey;
            student.Team = null;
        }

        StudentsInTeam.Clear();
    }

    public void WriteInfoTeam()
    {
        Console.WriteLine($"Id: {Id}");
        Console.WriteLine($"Name: {TeamName}");
        Console.Write("Members:");
        foreach (var student in StudentsInTeam)
        {
            Console.WriteLine($" {student.Id} {student.PersonName}");
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Team team &&
               Id == team.Id;
    }
}