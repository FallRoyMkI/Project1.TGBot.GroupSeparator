using System.ComponentModel.DataAnnotations;
using SeparatorIntoGroup.Options;
using static System.Net.Mime.MediaTypeNames;

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
    public void ClearGroup()
    {
        RemoveAllTeamsFromGroup();
        RemoveAllStudentsFromGroup();
    }

    public void CreateNewTeamInGroup(int id, string teamName)
    {
        Team team = new Team(id, teamName);
        TeamsInGroup.Add(team);
    }
    public void AddStudentToTeam(Team team, Student student)
    {
        team.AddStudentToTeam(student);
    }
    public void RemoveStudentFromTeam(Team team, Student student)
    {
        team.RemoveStudentFromTeam(student);
    }
    public void DeleteTeamFromGroup(Team team)
    {
        team.RemoveAllStudentsFromTeam();
        TeamsInGroup.Remove(team);
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


    public void WriteInfoGroup()
    {
        Console.WriteLine($"Name: {GroupName}");
        Console.WriteLine($"Count: {StudentsInGroup.Count}");
    }
    private double TimeComparison(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
    {
        if (!(start1 > end2 || start2 > end1))
        {
            return 0;
        }
        if (start1 < start2)
        {
            if (end1 > start2 && end1 < end2)
                return end1.Subtract(start2).TotalMinutes;
            if (end1 > end2)
                return end2.Subtract(start2).TotalMinutes;
        }
        else
        {
            if (end2 > start1 && end2 < end1)
                return end2.Subtract(start1).TotalMinutes;

            if (end2 > end1)
                return end1.Subtract(start1).TotalMinutes;
        }
        return 0;
    }
}