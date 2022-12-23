using System.Collections;
using System.Text.RegularExpressions;

namespace SeparatorIntoGroup.Tests.TestCaseSources;

public class ProjectCoreTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        List<Teacher> teachers = new List<Teacher>()
        {
            new Teacher(0,"AdminTeacher","@admin"),
            new Teacher(15,"TestTeacher","@test"),
            new Teacher(666,"QQQ","@QQQ")
        };

        List<Student> students = new List<Student>()
        {
            new Student(0, "TestStudent0", "@Test0"),
            new Student(1, "TestStudent1", "@Test1"),
            new Student(2, "TestStudent2", "@Test2"),
            new Student(3, "TestStudent3", "@Test3"),
            new Student(4, "TestStudent4", "@Test4"),
            new Student(5, "TestStudent5", "@Test5"),
        };

        List<Group> groups = new List<Group>()
        {
            new Group(0, "TestGroup"),
            new Group(10, "AdminGroup")
        };

        yield return new Object[] { teachers, students, groups };

        groups[1].AddStudentToGroup(students[0]);
        groups[1].AddStudentToGroup(students[1]);
        groups[1].AddStudentToGroup(students[2]);
        groups[1].AddStudentToGroup(students[3]);
        groups[1].CreateNewTeamInGroup(0,"TestTeam");
        groups[1].AddStudentToTeam(groups[1].TeamsInGroup[0], students[0]);
        groups[1].AddStudentToTeam(groups[1].TeamsInGroup[0], students[1]);
        groups[1].CreateNewTeamInGroup(1,"AdminTeam");
        groups[1].AddStudentToTeam(groups[1].TeamsInGroup[1], students[3]);

        yield return new Object[] { teachers, students, groups };

        groups[0].AddStudentToGroup(students[4]);
        groups[0].AddStudentToGroup(students[5]);
        groups[0].CreateNewTeamInGroup(2, "Test");
        groups[0].AddStudentToTeam(groups[0].TeamsInGroup[0], students[4]);
        groups[0].AddStudentToTeam(groups[0].TeamsInGroup[0], students[5]);
        groups[1].RemoveStudentFromTeam(groups[1].TeamsInGroup[0], groups[1].StudentsInGroup[1]);
        groups[0].RemoveStudentFromGroup(students[5]);

        yield return new Object[] { teachers, students, groups };
    }
}
