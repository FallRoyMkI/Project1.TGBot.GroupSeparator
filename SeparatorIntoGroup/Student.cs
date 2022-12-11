using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class Student: AbstractPersons
{
    public Student(int id, string name, string userName)
    {
        Id = id;
        PersonName = name;
        AccountName = userName;
        Status = StatusType.NotInGroup;
    }
}