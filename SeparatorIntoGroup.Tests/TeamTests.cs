using SeparatorIntoGroup.Options;
using SeparatorIntoGroup.Tests.TestCaseSources;

namespace SeparatorIntoGroup.Tests
{
    public class TeamTests
    {
        private Team _team;
        private ProjectCore _pc;

        [SetUp]
        public void Setup()
        {
            _team = new Team(0, "TestTeam");
            _pc = ProjectCore.GetProjectCore();

            _pc.Students.Add(new Student(0, "Гектор", "@gek"));
            _pc.Students.Add(new Student(1, "Рузвальд", "@ruzz"));

            _team.StudentsInTeam.AddRange(_pc.Students);
            _team.StudentsInTeam[0].Status = StatusType.InTeam;
            _team.StudentsInTeam[0].TeamId = _team.Id;
            _team.StudentsInTeam[1].Status = StatusType.InTeam;
            _team.StudentsInTeam[1].TeamId = _team.Id;

            _pc.Students.Add(new Student(2, "Руслан", "@russ"));
        }

        [TestCaseSource(typeof(TeamTestsSources))]
        public void AddStudentToTeamTest(Student student)
        {
            Student expectedStudent = new Student(student.Id, student.PersonName, student.AccountName);
            expectedStudent.TeamId = _team.Id;
            expectedStudent.Status = StatusType.InTeam;
            expectedStudent.GroupId = student.GroupId;
            expectedStudent.AnswersToQuestionnaire = student.AnswersToQuestionnaire;
            
            Student actualStudent = student;

            List<Student> expectedStudentsInTeam = new List<Student>();
            expectedStudentsInTeam.AddRange(_team.StudentsInTeam);
            expectedStudentsInTeam.Add(expectedStudent);

            List<Student> actualStudentsInTeam = _team.StudentsInTeam;

            _team.AddStudentToTeam(student);

            CollectionAssert.AreEqual(expectedStudentsInTeam, actualStudentsInTeam);
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [TestCaseSource(typeof(TeamTestsSources))]
        public void RemoveStudentFromTeamTest(Student student)
        {
            student.TeamId = _team.Id;
            student.Status = StatusType.InTeam;

            Student expectedStudent = new Student(student.Id, student.PersonName, student.AccountName);
            expectedStudent.Status = student.Status;
            expectedStudent.TeamId = student.TeamId;
            expectedStudent.GroupId = student.GroupId;
            expectedStudent.AnswersToQuestionnaire = student.AnswersToQuestionnaire;
            if (_team.StudentsInTeam.Contains(student))
            {
                expectedStudent.TeamId = -1;
                expectedStudent.Status = StatusType.PassedSurvey;
            }

            Student actualStudent = student;
            
            List<Student> expectedStudentsInTeam = new List<Student>();
            expectedStudentsInTeam.AddRange(_team.StudentsInTeam);
            expectedStudentsInTeam.Remove(student);

            List<Student> actualStudentsInTeam = _team.StudentsInTeam;
            
            _team.RemoveStudentFromTeam(student);

            CollectionAssert.AreEqual(expectedStudentsInTeam, actualStudentsInTeam);
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [Test]
        public void RemoveAllStudentFromTeamTest()
        {
            List<Student> expectedStudents = new List<Student>();
            foreach (var student in _pc.Students.FindAll(x => x.TeamId == _team.Id))
            {
                Student expectedStudent = new Student(student.Id, student.PersonName, student.AccountName);
                expectedStudent.Status = StatusType.PassedSurvey;
                expectedStudent.TeamId = -1;
                expectedStudent.GroupId = student.GroupId;
                expectedStudent.AnswersToQuestionnaire = student.AnswersToQuestionnaire;
                expectedStudents.Add(expectedStudent);
            }

            List<Student> actualStudents = new List<Student>();
            actualStudents.AddRange(_team.StudentsInTeam);

            _team.RemoveAllStudentsFromTeam();

            List<Student> expectedStudentsInTeam = new List<Student>();
            List<Student> actualStudentsInTeam = _team.StudentsInTeam;

            CollectionAssert.AreEqual(expectedStudentsInTeam, actualStudentsInTeam);
            CollectionAssert.AreEqual(expectedStudents, actualStudents);
        }

        [TearDown]
        public void TearDown()
        {
            _team = new Team(0, "TestTeam");
            _pc.Students = new List<Student>();
        }
    }
}