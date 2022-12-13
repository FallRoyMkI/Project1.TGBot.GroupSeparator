using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Teacher : AbstractPersons
{
    private ProjectCore _storage;

    public Teacher(int id, string name, string userName)
    {
        Id = id;
        PersonName = name;
        AccountName = userName;
        Status = StatusType.IsTeacher;
    }

    public void CreateNewStudent(int id, string name, string userName)
    {
        Student student = new Student(id, name, userName);
        _storage.Students.Add(student);
        _storage.SaveAll();
    }
    public void DeleteStudent(Student student)
    {
        _storage.Students.Remove(student);
        _storage.SaveAll();
    }


    public void CreateNewGroup(int id, string name)
    {
        Group group = new Group(id, name);
        _storage.Groups.Add(group);
        _storage.SaveAll();
    }
    public void AddStudentToGroup(Group group, Student student)
    {
        group.AddStudentToGroup(student);
        _storage.SaveAll();
    }
    public void RemoveStudentFromGroup(Group group, Student student)
    {
        group.RemoveStudentFromGroup(student);
        _storage.SaveAll();
    }
    public void DeleteGroup(Group group)
    {
        group.ClearGroup();
        _storage.Groups.Remove(group);
        _storage.SaveAll();
    }


    public void CreateNewTeamInGroup(Group group, int id, string teamName)
    {
        group.CreateNewTeamInGroup(id,teamName);
        _storage.SaveAll();
    }
    public void AddStudentToTeam(Group group, Team team, Student student)
    {
        group.AddStudentToTeam(team, student);
        _storage.SaveAll();
    }
    public void RemoveStudentFromTeam(Group group, Team team, Student student)
    {
        group.RemoveStudentFromTeam(team,student);
        _storage.SaveAll();
    }
    public void DeleteTeamFromGroup(Group group, Team team)
    {
        group.DeleteTeamFromGroup(team);
        _storage.SaveAll();
    }
}