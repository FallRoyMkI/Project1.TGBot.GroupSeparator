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
    public void AddStudentToGroupList(Student student)
    {
        StudentsInGroup.Add(student);
    }

    public void RemoveStudentFromGroupList(Student student)
    {
        StudentsInGroup.Remove(student);
    }

    public void RemoveAllStudentsFromGroupList()
    {
        StudentsInGroup.Clear();
    }
}