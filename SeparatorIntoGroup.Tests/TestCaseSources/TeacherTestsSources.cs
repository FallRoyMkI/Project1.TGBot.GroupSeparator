using System.Collections;
using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup.Tests.TestCaseSources;

public class StudentForCreationTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        long id = 0;
        string name = "Виктор";
        string userName = "@vitya";
        yield return new Object[] { id, name, userName };

        id = 1;
        name = "Альберт";
        userName = "@albi";
        yield return new Object[] { id, name, userName };
        
        id = 100;
        name = "Рузвальд";
        userName = "@ruzz";
        yield return new Object[] { id, name, userName };
    }
}

public class StudentForDeletingTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Student student = new Student(0, "Виктор", "@vitya");
        yield return new Object[] { student };

        student = new Student(1, "Альберт", "@albi");
        yield return new Object[] { student };

        student = new Student(100, "Рузвальд", "@ruzz");
        yield return new Object[] { student };
    }
}

public class GroupForCreationTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        int id = 5;
        string name = "Test";
        
        yield return new Object[] { id, name };

        id = 1;
        name = "TTeam";
        
        yield return new Object[] { id, name };

        id = 100;
        name = "TTT";
        yield return new Object[] { id, name };
    }
}

public class AddStudentToGroupTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Group group = new Group(0, "TestGroup");
        Student student = new Student(0, "Адольф", "@adik");

        yield return new Object[] { group, student };

        group = new Group(15, "AdminGroup");
        group.AddStudentToGroup(new Student(0, "Адольф", "@adik"));
        group.AddStudentToGroup(new Student(5, "Зургуль", "@Zrg"));
        student = new Student(1, "Creeper", "@boom");

        yield return new Object[] { group, student };
    }
}

public class RemoveStudentFromGroupTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Group group = new Group(0, "TestGroup");
        group.AddStudentToGroup(new Student(999, "НИКОЛАЙ НИКОЛАЙ КОЛЯ", "@trh"));
        group.AddStudentToGroup(new Student(0, "Адольф", "@adik"));
       
        yield return new Object[] { group, group.StudentsInGroup[1] };

        group = new Group(0, "TestGroup");
        group.AddStudentToGroup(new Student(0, "Адольф", "@adik"));
        Student student = new Student(1, "Creeper", "@boom");

        yield return new Object[] { group, student };

        group = new Group(15, "AdminGroup");
        group.AddStudentToGroup(new Student(0, "Адольф", "@adik"));
        group.AddStudentToGroup(new Student(5, "Зургуль", "@Zrg"));
        group.AddStudentToGroup(new Student(1, "Creeper", "@boom"));
        student = new Student(1, "Creeper", "@boom");
        student.GroupId = group.Id;
        student.Status = StatusType.InGroup;

        yield return new Object[] { group, student };
    }
}

public class DeleteGroupTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Group group = new Group(0, "TestGroup");
        group.AddStudentToGroup(new Student(999, "НИКОЛАЙ НИКОЛАЙ КОЛЯ", "@trh"));
        group.AddStudentToGroup(new Student(0, "Адольф", "@adik"));
        

        yield return new Object[] { group };

        group = new Group(0, "TestGroup");

        yield return new Object[] { group };

        group = new Group(15, "AdminGroup");
        group.AddStudentToGroup(new Student(0, "Адольф", "@adik"));
        group.AddStudentToGroup(new Student(5, "Зургуль", "@Zrg"));
        group.AddStudentToGroup(new Student(1, "Creeper", "@boom"));

        yield return new Object[] { group };
    }
}

public class CreateNewTeamInGroupTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Group group = new Group(0, "TestGroup");
        group.CreateNewTeamInGroup(14,"JustForTest");
        int id = 5;
        string name = "TeamFive";
        
        yield return new Object[] { group, id, name };

        group = new Group(0, "TestGroup");
        id = 17;
        name = "";
        
        yield return new Object[] { group, id, name };

        group = new Group(15, "AdminGroup");
        id = 0;
        name = "TeamZero";

        yield return new Object[] { group, id, name };
    }
}

public class TeacherAddStudentToTeamTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Group group = new Group(0, "TestGroup");
        group.CreateNewTeamInGroup(14, "JustForTest");
        Student student = new Student(0, "Test", "@test");

        yield return new Object[] { group, group.TeamsInGroup[0], student };

        group = new Group(2, "TestTTGroup"); ;
        group.CreateNewTeamInGroup(0,"TestTeam");
        group.CreateNewTeamInGroup(5,"Test");
        group.CreateNewTeamInGroup(88,"Admin");
        student = new Student(777, "KTO Я", "@WHOIAM");

        yield return new Object[] { group, group.TeamsInGroup[2], student };
    }
}

public class TeacherRemoveStudentToTeamTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Group group = new Group(0, "TestGroup");
        group.CreateNewTeamInGroup(14, "JustForTest");
        Student student = new Student(0, "Test", "@test");
        group.AddStudentToTeam(group.TeamsInGroup[0], student);

        yield return new Object[] { group, group.TeamsInGroup[0], student };

        group = new Group(2, "TestTTGroup"); ;
        group.CreateNewTeamInGroup(0, "TestTeam");
        group.CreateNewTeamInGroup(5, "Test");
        group.CreateNewTeamInGroup(88, "Admin");
        group.AddStudentToTeam(group.TeamsInGroup[2], new Student(777, "KTO Я", "@WHOIAM"));
        group.AddStudentToTeam(group.TeamsInGroup[2], new Student(0, "Test", "@test"));

        yield return new Object[] { group, group.TeamsInGroup[2], group.TeamsInGroup[2].StudentsInTeam[0] };
    }
}

public class DeleteTeamFromGroupTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Group group = new Group(0, "TestGroup");
        group.CreateNewTeamInGroup(14, "JustForTest");
        group.CreateNewTeamInGroup(0, "TestTeam");
        group.CreateNewTeamInGroup(5, "Test");

        yield return new Object[] { group, group.TeamsInGroup[2] };

        group = new Group(0, "TestGroup");
        group.CreateNewTeamInGroup(88, "Admin");
        group.AddStudentToTeam(group.TeamsInGroup[0], new Student(777, "KTO Я", "@WHOIAM"));
        group.AddStudentToTeam(group.TeamsInGroup[0], new Student(0, "Test", "@test"));

        yield return new Object[] { group, group.TeamsInGroup[0] };
    }
}