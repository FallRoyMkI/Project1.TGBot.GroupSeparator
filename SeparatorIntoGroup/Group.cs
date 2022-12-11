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
        student.Group = this;
    }

    public void RemoveStudentFromGroup(Student student)
    {
        StudentsInGroup.Remove(student);
        student.Status = StatusType.NotInGroup;
        student.Group = null;
    }

    public void DeleteGroup()
    {
        RemoveAllTeamsFromGroup();
        RemoveAllStudentsFromGroup();
    }
    private void RemoveAllStudentsFromGroup()
    {
        foreach (var student in StudentsInGroup)
        {
            student.Status = StatusType.NotInGroup;
            student.Group = null;
        }

        StudentsInGroup.Clear();
    }
    private void RemoveAllTeamsFromGroup()
    {
        foreach (var team in TeamsInGroup)
        {
            team.RemoveAllStudentsFromTeam();
        }

        TeamsInGroup.Clear();
    }

    public void CreateTeamToGroup(int id, string teamName)
    {
        Team team = new Team(id, teamName);
        TeamsInGroup.Add(team);
    }

    public void RemoveTeamFromGroup(Team team)
    {
        team.RemoveAllStudentsFromTeam();
        TeamsInGroup.Remove(team);
    }

    public void AddStudentToTeam(Team team, Student student)
    {
        team.AddStudentToTeam(student);
    }

    public void RemoveStudentFromTeam(Team team, Student student)
    {
        team.RemoveStudentFromTeam(student);
    }

    public void WriteInfoGroup()
    {
        Console.WriteLine($"Name: {GroupName}");
        Console.WriteLine($"Count: {StudentsInGroup.Count}");
    }
}