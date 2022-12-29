using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace SeparatorIntoGroup.Tests.TestCaseSources;

public class StudentsForGroupTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Student student = new Student(0, "Гектор", "@gek");
        yield return new Object[] { student };

        student = new Student(4, "Альберт", "@albi");
        yield return new Object[] { student };

        student = new Student(1, "Рузвальд", "@ruzz");
        yield return new Object[] { student };
    }
}

public class TeamsForCreationTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        int id = 5;
        string name = "TeamFive";
        yield return new Object[] { id, name };

        id = 0;
        name = "TeamZero";
        yield return new Object[] { id, name };

        id = 17;
        name = "";
        yield return new Object[] { id, name };
    }
}

public class AddStudentToTeamTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Team team = new Team(0, "TestTeam");
        team.AddStudent(new Student(99, "Гектор", "@gek"));
        Student student = new Student(13, "Test", "@test");
        
        yield return new Object[] { team, student };

        team = new Team(18, "");
        team.StudentsInTeam.Add(new Student(99, "Гриндевальд", "@grin"));
        team.StudentsInTeam.Add(new Student(100, "Рузвальд", "@ruzz"));

        yield return new Object[] { team, team.StudentsInTeam[1] };
    }
}

public class RemoveStudentFromTeamsInGroupTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Team team = new Team(0, "TestTeam");
        Student StudentForRemoving = new Student(13, "Cringe", "@Cig");
        team.AddStudent(new Student(99, "Гектор", "@gek"));
        team.AddStudent(StudentForRemoving);
        team.AddStudent(new Student(100, "Рузвальд", "@ruzz"));

        yield return new Object[] { team, StudentForRemoving };

        team = new Team(18, "");
        team.StudentsInTeam.Add(new Student(99, "Гектор", "@gek"));
        team.StudentsInTeam.Add(new Student(100, "Рузвальд", "@ruzz"));

        yield return new Object[] { team, team.StudentsInTeam[1] };

        team = new Team(11, "qwe");
        team.StudentsInTeam.Add(new Student(99, "Гектор", "@gek"));
        team.StudentsInTeam.Add(new Student(100, "Рузвальд", "@ruzz"));

        yield return new Object[] { team, StudentForRemoving };
    }
}

public class TeamsForRemoveTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Team team = new Team(0, "TestTeam");
        team.AddStudent(new Student(1, "Рузвальд", "@ruzz"));

        yield return new Object[] { team };

         team = new Team(18, "");
        team.AddStudent(new Student(99, "Гектор", "@gek"));
        team.AddStudent(new Student(100, "Рузвальд", "@ruzz"));

        yield return new Object[] { team };
    }
}

