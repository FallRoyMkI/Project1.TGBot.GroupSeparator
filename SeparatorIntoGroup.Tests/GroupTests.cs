using SeparatorIntoGroup.Options;
using SeparatorIntoGroup.Tests.TestCaseSources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeparatorIntoGroup.Tests
{
    public class GroupTests
    {
        private Group _group;
        private ProjectCore _pc;

        [SetUp]
        public void SetUp()
        {
            _group = new Group(0, "TestGroup");
            _pc = ProjectCore.GetProjectCore();

            _pc.Students.Add(new Student(0, "Гектор", "@gek"));
            _pc.Students.Add(new Student(1, "Рузвальд", "@ruzz"));

            _group.StudentsInGroup.AddRange(_pc.Students);
            _group.StudentsInGroup[0].Status = StatusType.InGroup;
            _group.StudentsInGroup[0].GroupId = _group.Id;
            _group.StudentsInGroup[1].Status = StatusType.InGroup;
            _group.StudentsInGroup[1].GroupId = _group.Id;

            _pc.Students.Add(new Student(3, "Руслан", "@russ"));

            _group.TeamsInGroup.Add(new Team(0,"TestTeam"));
            _group.TeamsInGroup[0].AddStudentToTeam(_group.StudentsInGroup[1]);
        }

        [TestCaseSource(typeof(StudentsForGroupTestsSources))]
        public void AddStudentToGroupTest(Student student)
        {
            Student expectedStudent = new Student(student.Id, student.PersonName, student.AccountName);
            expectedStudent.TeamId = -1;
            expectedStudent.Status = StatusType.InGroup;
            expectedStudent.GroupId = _group.Id;
            expectedStudent.AnswersToQuestionnaire = student.AnswersToQuestionnaire;

            Student actualStudent = student;

            List<Student> expectedStudentsInGroup = new List<Student>();
            expectedStudentsInGroup.AddRange(_group.StudentsInGroup);
            expectedStudentsInGroup.Add(expectedStudent);

            List<Student> actualStudentsInGroup = _group.StudentsInGroup;

            _group.AddStudentToGroup(student);

            CollectionAssert.AreEqual(expectedStudentsInGroup, actualStudentsInGroup);
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [TestCaseSource(typeof(StudentsForGroupTestsSources))]
        public void RemoveStudentFromGroupTestWhyHeNotInTeam(Student student)
        {
            student.GroupId = _group.Id;
            student.Status = StatusType.InGroup;

            Student expectedStudent = new Student(student.Id, student.PersonName, student.AccountName);
            expectedStudent.Status = student.Status;
            expectedStudent.TeamId = student.TeamId;
            expectedStudent.GroupId = student.GroupId;
            expectedStudent.AnswersToQuestionnaire = student.AnswersToQuestionnaire;
            if (_group.StudentsInGroup.Contains(student))
            {
                expectedStudent.GroupId = -1;
                expectedStudent.Status = StatusType.NotInGroup;
            }

            Student actualStudent = student;

            List<Student> expectedStudentsInGroup = new List<Student>();
            expectedStudentsInGroup.AddRange(_group.StudentsInGroup);
            expectedStudentsInGroup.Remove(student);

            List<Student> actualStudentsInGroup = _group.StudentsInGroup;
            
            _group.RemoveStudentFromGroup(student);

            CollectionAssert.AreEqual(expectedStudentsInGroup, actualStudentsInGroup);
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [Test]
        public void RemoveStudentFromGroupTestWhenHeInTeam()
        {
            Student student = _group.StudentsInGroup[1];

            Student expectedStudent = new Student(student.Id, student.PersonName, student.AccountName);
            expectedStudent.Status = student.Status;
            expectedStudent.TeamId = student.TeamId;
            expectedStudent.GroupId = student.GroupId;
            expectedStudent.AnswersToQuestionnaire = student.AnswersToQuestionnaire;
            if (_group.StudentsInGroup.Contains(student))
            {
                expectedStudent.GroupId = -1;
                expectedStudent.TeamId = -1;
                expectedStudent.Status = StatusType.NotInGroup;
            }

            Student actualStudent = student;

            List<Student> expectedStudentsInGroup = new List<Student>();
            expectedStudentsInGroup.AddRange(_group.StudentsInGroup);
            expectedStudentsInGroup.Remove(student);

            List<Student> actualStudentsInGroup = _group.StudentsInGroup;

            _group.RemoveStudentFromGroup(student);

            CollectionAssert.AreEqual(expectedStudentsInGroup, actualStudentsInGroup);
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [Test]
        public void ClearGroupTest()
        {
            List<Student> expectedStudents = new List<Student>();
            foreach (var student in _pc.Students.FindAll(x => x.GroupId == _group.Id))
            {
                Student expectedStudent = new Student(student.Id, student.PersonName, student.AccountName);
                expectedStudent.Status = StatusType.NotInGroup;
                expectedStudent.TeamId = -1;
                expectedStudent.GroupId = -1;
                expectedStudent.AnswersToQuestionnaire = student.AnswersToQuestionnaire;
                expectedStudents.Add(expectedStudent);
            }

            List<Student> actualStudents = new List<Student>();
            actualStudents.AddRange(_group.StudentsInGroup);

            _group.ClearGroup();

            List<Student> expectedStudentsInGroup = new List<Student>();
            List<Student> actualStudentsInGroup = _group.StudentsInGroup;

            List<Team> expectedTeams = new List<Team>();
            List<Team> actualTeams = _group.TeamsInGroup;

            CollectionAssert.AreEqual(expectedStudentsInGroup, actualStudentsInGroup);
            CollectionAssert.AreEqual(expectedTeams, actualTeams);
            CollectionAssert.AreEqual(expectedStudents, actualStudents);
        }

        [TestCaseSource(typeof(TeamsForCreationTestsSources))]
        public void CreateNewTeamInGroupTest(int id, string teamName)
        {
            List<Team> expectedTeams = new List<Team>();
            expectedTeams.AddRange(_group.TeamsInGroup);
            expectedTeams.Add(new Team(id,teamName));

            _group.CreateNewTeamInGroup(id,teamName);
            List<Team> actualTeams = _group.TeamsInGroup;

            CollectionAssert.AreEqual(expectedTeams, actualTeams);
        }

        [TestCaseSource(typeof(AddStudentToTeamTestsSources))]
        public void AddStudentToTeamTest(Team team, Student student)
        {
            List<Student> expectedStudents = new List<Student>();
            expectedStudents.AddRange(team.StudentsInTeam);
            expectedStudents.Add(student);
            List<Student> actualStudents = team.StudentsInTeam;

            _group.AddStudentToTeam(team,student);

            CollectionAssert.AreEqual(expectedStudents, actualStudents);
        }

        [TestCaseSource(typeof(RemoveStudentFromTeamsInGroupTestsSources))]
        public void RemoveStudentFromTeamTest(Team team, Student student)
        {
            List<Student> expectedStudentsInTeam = new List<Student>();
            expectedStudentsInTeam.AddRange(team.StudentsInTeam);
            expectedStudentsInTeam.Remove(student);
            List<Student> teamStudentsInTeam = team.StudentsInTeam;

            _group.RemoveStudentFromTeam(team, student);

            CollectionAssert.AreEqual(expectedStudentsInTeam, teamStudentsInTeam);
        }

        [TestCaseSource(typeof(TeamsForRemoveTestsSources))]
        public void DeleteTeamFromGroupTestWhenTeamNotInThisGroup(Team team)
        {
            List<Team> expectedTeams = new List<Team>();
            expectedTeams.AddRange(_group.TeamsInGroup);
            expectedTeams.Remove(team);
            List<Team> actualTeams = _group.TeamsInGroup;

            _group.DeleteTeamFromGroup(team);

            CollectionAssert.AreEqual(expectedTeams, actualTeams);
        }

        [Test]
        public void DeleteTeamFromGroupTestWhenTeamInThisGroup()
        {
            Team team = _group.TeamsInGroup[0];
            List<Team> expectedTeams = new List<Team>();
            expectedTeams.AddRange(_group.TeamsInGroup);
            expectedTeams.Remove(team);
            List<Team> actualTeams = _group.TeamsInGroup;

            _group.DeleteTeamFromGroup(team);

            CollectionAssert.AreEqual(expectedTeams, actualTeams);
        }

        [TearDown]
        public void TearDown()
        {
            _group = new Group(0, "TestTeam");
            _pc.Students = new List<Student>();
        }
    }
}
