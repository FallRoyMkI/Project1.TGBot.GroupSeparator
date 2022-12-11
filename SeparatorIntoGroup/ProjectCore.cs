namespace SeparatorIntoGroup;

public class ProjectCore
{
    public List<Student> Students { get; set; }
    public List<Group> Groups { get; set; }
    public List<Teacher> Teachers { get; set; }

    public ProjectCore() 
    { 
        Students = new List<Student>();
        Groups = new List<Group>();
        Teachers= new List<Teacher>();
    }
}