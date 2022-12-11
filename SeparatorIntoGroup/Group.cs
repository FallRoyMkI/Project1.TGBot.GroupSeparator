using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Group
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public List<Student> StudentsInGroup { get; set; }
    public List<Team> TeamsInGroup { get; set; }

    public Group(int id, string name)
    {
        Id = id;
        GroupName = name;
        StudentsInGroup = new List<Student>();
        TeamsInGroup = new List<Team>();
    }

    public void AddStudentToGroup(Student student)
    {
        StudentsInGroup.Add(student);
        student.Status = StatusType.InGroup;
    }

    public void RemoveStudentFromGroup(Student student)
    {
        StudentsInGroup.Remove(student);
        student.Status = StatusType.NotInGroup;
    }

    public void RemoveAllStudentsFromGroup()
    {
        foreach (var student in StudentsInGroup)
        {
            student.Status = StatusType.NotInGroup;
        }
        StudentsInGroup.Clear();
    }

    public void AddTeamToGroup(Team team)
    {
        TeamsInGroup.Add(team);
    }

    public void RemoveTeamFromGroup(Team team)
    {
        TeamsInGroup.Remove(team);
    }

    public void RemoveAllTeamsFromGroup()
    {
        TeamsInGroup.Clear();
    }

    public void AddStudentToTeam(Team team, Student student)
    {
        team.AddStudentToTeam(student);
    }

    public void RemoveStudentFromTeam(Team team, Student student)
    {
        team.RemoveStudentFromTeam(student);
    }
}