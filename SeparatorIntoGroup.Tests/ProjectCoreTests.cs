using SeparatorIntoGroup.Tests.TestCaseSources;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SeparatorIntoGroup.Tests;

public class ProjectCoreTests
{
    public string Path;
    private ProjectCore _pc;
    [SetUp]
    public void SetUp()
    {
        Path = "../../../Test.txt";
        _pc = ProjectCore.GetProjectCore();
        _pc.SetPathForTests(Path);
    }
    
    [TestCaseSource(typeof(ProjectCoreTestsSources))]
    public void SaveAllTest(List<Teacher> teachers, List<Student> students, List<Group> groups)
    {
        List<Teacher> expectedTeachers = teachers;
        List<Student> expectedStudents = students;
        List<Group> expectedGroups = groups;
        
        _pc.Teachers = teachers;
        _pc.Students = students;
        _pc.Groups = groups;
        _pc.SaveAll();

        using (StreamReader sr = new StreamReader(Path))
        {
            string jsn = sr.ReadLine();
            _pc.Teachers = JsonSerializer.Deserialize<List<Teacher>>(jsn);
            jsn = sr.ReadLine();
            _pc.Students = JsonSerializer.Deserialize<List<Student>>(jsn);
            jsn = sr.ReadLine();
            _pc.Groups = JsonSerializer.Deserialize<List<Group>>(jsn);
        }

        List<Teacher> actualTeachers = _pc.Teachers;
        List<Student> actualStudents = _pc.Students;
        List<Group> actualGroups = _pc.Groups;

        CollectionAssert.AreEqual(expectedTeachers,actualTeachers);
        CollectionAssert.AreEqual(expectedStudents,actualStudents);
        CollectionAssert.AreEqual(expectedGroups,actualGroups);
    }

    [TestCaseSource(typeof(ProjectCoreTestsSources))]
    public void LoadAllTest(List<Teacher> teachers, List<Student> students, List<Group> groups)
    {
        List<Teacher> expectedTeachers = teachers;
        List<Student> expectedStudents = students;
        List<Group> expectedGroups = groups;

        using (StreamWriter sw = new StreamWriter(Path))
        {
            string jsn = JsonSerializer.Serialize(teachers);
            sw.WriteLine(jsn);
            jsn = JsonSerializer.Serialize(students);
            sw.WriteLine(jsn);
            jsn = JsonSerializer.Serialize(groups);
            sw.WriteLine(jsn);
        }

        _pc.LoadAll();

        List<Teacher> actualTeachers = _pc.Teachers;
        List<Student> actualStudents = _pc.Students;
        List<Group> actualGroups = _pc.Groups;

        CollectionAssert.AreEqual(expectedTeachers, actualTeachers);
        CollectionAssert.AreEqual(expectedStudents, actualStudents);
        CollectionAssert.AreEqual(expectedGroups, actualGroups);
    }

    [Test]
    public void GetProjectCoreTest()
    {
        ProjectCore expectedCore = ProjectCore.GetProjectCore();
        ProjectCore actualCore = ProjectCore.GetProjectCore();

        actualCore.Students.Add(new Student(0,"qqq","qqq"));
        actualCore.Teachers.Add(new Teacher(0,"qqq","qqq"));
        actualCore.Groups.Add(new Group(0,"qqq"));

        List<Teacher> expectedTeachers = expectedCore.Teachers;
        List<Student> expectedStudents = expectedCore.Students;
        List<Group> expectedGroups = expectedCore.Groups;

        List<Teacher> actualTeachers = actualCore.Teachers;
        List<Student> actualStudents = actualCore.Students;
        List<Group> actualGroups = actualCore.Groups;

        CollectionAssert.AreEqual(expectedTeachers, actualTeachers);
        CollectionAssert.AreEqual(expectedStudents, actualStudents);
        CollectionAssert.AreEqual(expectedGroups, actualGroups);
    }

    [TearDown]
    public void TearDown()
    {
        File.Delete(Path);
        _pc.Teachers.Clear();
        _pc.Students.Clear();
        _pc.Groups.Clear();
        Path = "../../../Storage.txt";
        _pc.SetPathForTests(Path);
    }
}