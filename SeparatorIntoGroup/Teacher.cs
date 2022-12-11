using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Teacher : AbstractPersons
{
    public Teacher(int id, string name, string userName)
    {
        Id = id;
        PersonName = name;
        AccountName = userName;
        Status = StatusType.IsTeacher;
    }
}