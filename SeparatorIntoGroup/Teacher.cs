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

    public void AddStudent(Student student)
    {
        _storage.Students.Add(student);
        _storage.SaveAll();
    }

    public void DeleteStudent(Student student)
    {
        _storage.Students.Remove(student);
        _storage.SaveAll();
    }

    public void AddGroup(Group group)
    {
        _storage.Groups.Add(group);
        _storage.SaveAll();
    }

    public void DeleteGroup(Group group)
    {
        _storage.Groups.Remove(group);
        _storage.SaveAll();
    }
}