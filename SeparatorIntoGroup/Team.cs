using SeparatorIntoGroup.Options;
using System.Linq;

namespace SeparatorIntoGroup;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> StudentsInTeam { get; set; }

    public Team(int id, string name)
    {
        Id = id;
        Name = name;
        StudentsInTeam = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        StudentsInTeam.Add(student);
        student.Status = StatusType.InTeam;
        student.TeamId = Id;
    }

    public void RemoveStudent(Student student)
    {
        if (StudentsInTeam.Contains(student))
        {
            StudentsInTeam.Remove(student);
            student.Status = StatusType.PassedSurvey;
            student.TeamId = -1;
        }
    }

    public void RemoveAllStudents()
    {
        foreach (var student in StudentsInTeam)
        {
            student.Status = StatusType.PassedSurvey;
            student.TeamId = -1;
        }

        StudentsInTeam.Clear();
    }

    public override bool Equals(object? obj)
    {

        if (obj is Team)
        {
            List<Student> students = ((Team)obj).StudentsInTeam;
            if (StudentsInTeam.Count != students.Count)
            {
                return false;
            }

            for (int i = 0; i < StudentsInTeam.Count; i++)
            {
                if (!students[i].Equals(StudentsInTeam[i]))
                {
                    return false;
                }
            }
        }
        return obj is Team team &&
               Id == team.Id &&
               Name == team.Name;
    }
}