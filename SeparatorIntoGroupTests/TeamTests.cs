using SeparatorIntoGroup;
using SeparatorIntoGroupTests.TestCaseSource;

namespace SeparatorIntoGroupTests
{
    public class TeamTests
    {
        private Team _team;
        
        [SetUp]
        public void Setup()
        {
            _team = new Team(0, "TestTeam");
           
        }

        [TestCaseSource(typeof(TeamTestsSources))]
        public void AddStudentToTeamTest(Student student)
        {
            _team.AddStudentToTeam(student);
            List<Student> expectedStudents = new List<Student>() { student };
            List<Student> actualStudents = _team.StudentsInTeam;
            CollectionAssert.AreEqual(expectedStudents, actualStudents);
        }
    }
}