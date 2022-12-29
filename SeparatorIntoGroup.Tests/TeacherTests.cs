using SeparatorIntoGroup.Tests.TestCaseSources;
using System.Text.RegularExpressions;
using NUnit.Framework.Constraints;
using System.IO;

namespace SeparatorIntoGroup.Tests;

public class TeacherTests
{
    private ProjectCore _pc;
    private Teacher _teacher;
    public string Path;

    [SetUp]
    public void SetUp()
    {
        Path = "../../../Test.txt";
        _pc = ProjectCore.GetProjectCore();
        _pc.SetPathForTests(Path);
        _pc.Students = new List<Student>()
        { 
            new Student(0, "Виктор", "@vitya"),
            new Student(1, "Альберт", "@albi"),
            new Student(100, "Рузвальд", "@ruzz")
        };
        _pc.Groups = new List<Group>()
        {
            new Group(0,"TestGroup")
        };

        _pc.Groups[0].AddStudent(_pc.Students[0]);
        _pc.Groups[0].CreateNewTeam(0,"TestTeam");
        _pc.Groups[0].AddStudentToTeam(_pc.Groups[0].TeamsInGroup[0], _pc.Groups[0].StudentsInGroup[0]);

        _teacher = new Teacher(0, "admin", "@admin");
    }

    [TestCaseSource(typeof(StudentForCreationTestsSources))]
    public void CreateNewStudentTest(long id, string name, string userName)
    {
        List<Student> expectedStudents = new List<Student>();
        expectedStudents.AddRange(_pc.Students);
        expectedStudents.Add(new Student(id,name, "@"+userName));
        
        List<Student> actualStudents = _pc.Students;

        _teacher.CreateNewStudent(id,name,userName);

        CollectionAssert.AreEqual(expectedStudents, actualStudents);
    }

    [TestCaseSource(typeof(StudentForDeletingTestsSources))]
    public void DeleteStudentTest(Student student)
    {
        List<Student> expectedStudents = new List<Student>();
        expectedStudents.AddRange(_pc.Students);
        expectedStudents.Remove(student);

        _teacher.DeleteStudent(student);
        List<Student> actualStudents = _pc.Students;

        CollectionAssert.AreEqual(expectedStudents,actualStudents);
    }

    [TestCaseSource(typeof(GroupForCreationTestsSources))]
    public void CreateNewGroupTest(int id, string name)
    {
        List<Group> expectedGroups = new List<Group>();
        expectedGroups.AddRange(_pc.Groups);
        expectedGroups.Add(new Group(id,name));
        
        _teacher.CreateNewGroup(id,name);
        List<Group> actualGroups = _pc.Groups;

        CollectionAssert.AreEqual(expectedGroups,actualGroups);
    }

    [TestCaseSource(typeof(AddStudentToGroupTestsSources))]
    public void AddStudentToGroupTest(Group group, Student student)
    {
        List<Student> expectedStudents = new List<Student>();
        expectedStudents.AddRange(group.StudentsInGroup);
        expectedStudents.Add(student);

        List<Student> actualStudents = group.StudentsInGroup;

        _teacher.AddStudentToGroup(group,student);

        CollectionAssert.AreEqual(expectedStudents, actualStudents);
    }
    [TestCaseSource(typeof(RemoveStudentFromGroupTestsSources))]
    public void RemoveStudentFromGroupTest(Group group, Student student)
    {
        List<Student> expectedStudents = new List<Student>();
        expectedStudents.AddRange(group.StudentsInGroup);
        expectedStudents.Remove(student);

        List<Student> actualStudents = group.StudentsInGroup;

        _teacher.RemoveStudentFromGroup(group, student);

        CollectionAssert.AreEqual(expectedStudents, actualStudents);
    }

    [TestCaseSource(typeof(DeleteGroupTestsSources))]
    public void DeleteGroupTest(Group group)
    {
        List<Group> expectedGroups = new List<Group>();
        expectedGroups.AddRange(_pc.Groups);
        expectedGroups.Remove(group);

        List<Group> actualGroups =_pc.Groups;

        _teacher.DeleteGroup(group);

        CollectionAssert.AreEqual(expectedGroups, actualGroups);
    }

    [Test]
    public void DeleteGroupTest()
    {
        Group group = _pc.Groups[0];

        List<Group> expectedGroups = new List<Group>();
        expectedGroups.AddRange(_pc.Groups);
        expectedGroups.Remove(group);

        List<Group> actualGroups = _pc.Groups;

        _teacher.DeleteGroup(group);

        CollectionAssert.AreEqual(expectedGroups, actualGroups);
    }

    [TestCaseSource(typeof(CreateNewTeamInGroupTestsSources))]
    public void CreateNewTeamInGroupTest(Group group, int id, string teamName)
    {
        List<Team> expectedTeams = new List<Team>();
        expectedTeams.AddRange(group.TeamsInGroup);
        expectedTeams.Add(new Team(id,teamName));

        List<Team> actualTeams = group.TeamsInGroup;

        _teacher.CreateNewTeam(group,id,teamName);

        CollectionAssert.AreEqual(expectedTeams, actualTeams);
    }

    [TestCaseSource(typeof(TeacherAddStudentToTeamTestsSources))]
    public void AddStudentToTeamTest(Group group, Team team, Student student)
    {
        List<Student> expectedStudents = new List<Student>();
        expectedStudents.AddRange(team.StudentsInTeam);
        expectedStudents.Add(student);

        List<Student> actualStudents = team.StudentsInTeam;

        _teacher.AddStudentToTeam(group, team, student);

        CollectionAssert.AreEqual(expectedStudents, actualStudents);
    }

    [TestCaseSource(typeof(TeacherRemoveStudentToTeamTestsSources))]
    public void RemoveStudentFromTeamTest(Group group, Team team, Student student)
    {
        List<Student> expectedStudents = new List<Student>();
        expectedStudents.AddRange(team.StudentsInTeam);
        expectedStudents.Remove(student);

        List<Student> actualStudents = team.StudentsInTeam;

        _teacher.RemoveStudentFromTeam(group, team, student);

        CollectionAssert.AreEqual(expectedStudents, actualStudents);
    }

    [TestCaseSource(typeof(DeleteTeamFromGroupTestsSources))]
    public void DeleteTeamFromGroupTest(Group group, Team team)
    {
        List<Team> expectedTeams = new List<Team>();
        expectedTeams.AddRange(group.TeamsInGroup);
        expectedTeams.Remove(team);

        List<Team> actualTeams = group.TeamsInGroup;

        _teacher.DeleteTeamFromGroup(group, team);

        CollectionAssert.AreEqual(expectedTeams, actualTeams);
    }

    [TearDown]
    public void TearDown()
    {
        File.Delete(Path);
        _pc.Students.Clear();
        _pc.Groups.Clear();
        _pc.Teachers.Clear();
        Path = "../../../Storage.txt";
        _pc.SetPathForTests(Path);
    }
}