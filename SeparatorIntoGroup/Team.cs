namespace SeparatorIntoGroup;

public class Team
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public List<Student> StudentsInTeam { get; set; }

    public Team(int id, string name)
    {
        Id = id;
        TeamName = name;
        StudentsInTeam = new List<Student>();
    }

    public void AddStudentToTeamList(Student student)
    {
        StudentsInTeam.Add(student);
    }

    public void RemoveStudentFromTeamList(Student student)
    {
        StudentsInTeam.Remove(student);
    }

    public void RemoveAllStudentsFromTeamList()
    {
        StudentsInTeam.Clear();
    }
}