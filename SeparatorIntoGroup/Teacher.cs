using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Teacher : AbstractPersons
{
    private ProjectCore _projectCore = ProjectCore.GetProjectCore();

    public Teacher(long id, string personName, string accountName)
    {
        Id = id;
        PersonName = personName;
        AccountName = accountName;
        Status = StatusType.IsTeacher;
    }

    public void CreateNewStudent(long id, string name, string userName)
    {
        Student student = new Student(id, name, "@"+userName);
        _projectCore.Students.Add(student);
        _projectCore.SaveAll();
    }
    public void DeleteStudent(Student student)
    {
        _projectCore.Students.Remove(student);
        _projectCore.SaveAll();
    }

    public void CreateNewGroup(long id, string name)
    {
        Group group = new Group(id, name);
        _projectCore.Groups.Add(group);
        _projectCore.SaveAll();
    }
    public void AddStudentToGroup(Group group, Student student)
    {
        group.AddStudent(student);
        _projectCore.SaveAll();
    }
    public void RemoveStudentFromGroup(Group group, Student student)
    {
        group.RemoveStudent(student);
        _projectCore.SaveAll();
    }
    public void DeleteGroup(Group group)
    {
        group.DeleteGroup();
        _projectCore.Groups.Remove(group);
        _projectCore.SaveAll();
    }


    public void CreateNewTeam(Group group, int id, string name)
    {
        group.CreateNewTeam(id,name);
        _projectCore.SaveAll();
    }
    public void AddStudentToTeam(Group group, Team team, Student student)
    {
        group.AddStudentToTeam(team, student);
        _projectCore.SaveAll();
    }
    public void RemoveStudentFromTeam(Group group, Team team, Student student)
    {
        group.RemoveStudentFromTeam(team,student);
        _projectCore.SaveAll();
    }
    public void DeleteTeamFromGroup(Group group, Team team)
    {
        group.DeleteTeamFromGroup(team);
        _projectCore.SaveAll();
    }

    public override bool Equals(object? obj)
    {
        return obj is Teacher teacher &&
               Id == teacher.Id &&
               PersonName == teacher.PersonName &&
               AccountName == teacher.AccountName &&
               Status == teacher.Status;
    }
}