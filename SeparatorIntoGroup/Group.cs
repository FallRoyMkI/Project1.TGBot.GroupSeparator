using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using SeparatorIntoGroup.Options;
using static System.Net.Mime.MediaTypeNames;

namespace SeparatorIntoGroup;

public class Group
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Student> StudentsInGroup { get; set; }
    public List<Team> TeamsInGroup { get; set; }

    public Group(long id, string name)
    {
        Id = id;
        Name = name;
        StudentsInGroup = new List<Student>();
        TeamsInGroup = new List<Team>();
    }

    public void AddStudentToGroup(Student student)
    {
        StudentsInGroup.Add(student);
        student.Status = StatusType.InGroup;
        student.GroupId = Id;
    }
    public void RemoveStudentFromGroup(Student student)
    {
        if (StudentsInGroup.Contains(student))
        {
            if (student.TeamId != -1)
            {
                Team team = TeamsInGroup.Find(x => x.Id == student.TeamId);
                RemoveStudentFromTeam(team, student);
            }
            StudentsInGroup.Remove(student);
            student.Status = StatusType.NotInGroup;
            student.GroupId = -1;
        }
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
        if (TeamsInGroup.Contains(team))
        {
            team.RemoveAllStudentsFromTeam();
            TeamsInGroup.Remove(team);
        }
    }


    private void RemoveAllTeamsFromGroup()
    {
        foreach (var team in TeamsInGroup)
        {
            team.RemoveAllStudentsFromTeam();
        }

        TeamsInGroup.Clear();
    }
    private void RemoveAllStudentsFromGroup()
    {
        foreach (var student in StudentsInGroup)
        {
            student.Status = StatusType.NotInGroup;
            student.GroupId = -1;
        }

        StudentsInGroup.Clear();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is Group)
        {
            List<Student> studentsInGroup = ((Group)obj).StudentsInGroup;
            if (StudentsInGroup.Count != studentsInGroup.Count)
            {
                return false;
            }

            for (int i = 0; i < StudentsInGroup.Count; i++)
            {
                if (!studentsInGroup[i].Equals(StudentsInGroup[i]))
                {
                    return false;
                }
            }
        }
        if (obj is Group)
        {
            List<Team> teamsInGroup = ((Group)obj).TeamsInGroup;
            if (TeamsInGroup.Count != teamsInGroup.Count)
            {
                return false;
            }

            for (int i = 0; i < TeamsInGroup.Count; i++)
            {
                if (!teamsInGroup[i].Equals(TeamsInGroup[i]))
                {
                    return false;
                }
            }
        }

        return obj is Group group &&
               Id == group.Id &&
               Name == group.Name;
    }
}