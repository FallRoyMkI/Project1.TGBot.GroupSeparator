using System.Text.Json;

namespace SeparatorIntoGroup;

public class ProjectCore
{
    public List<Teacher> Teachers { get; set; }
    public List<Student> Students { get; set; }
    public List<Group> Groups { get; set; }

    private string _path { get; set; }
    private static ProjectCore _projectCore;

    private ProjectCore()
    {
        Teachers = new List<Teacher>();
        Students = new List<Student>();
        Groups = new List<Group>();
        _path = "../../../Storage.txt";
    }

    public static ProjectCore GetProjectCore()
    {
        if (_projectCore == null)
        {
           _projectCore = new ProjectCore();
        }

        return _projectCore;
    }

    public void SaveAll()
    {
        using (StreamWriter sw = new StreamWriter(_path))
        {
            string jsn = JsonSerializer.Serialize(Teachers);
            sw.WriteLine(jsn);
            jsn = JsonSerializer.Serialize(Students);
            sw.WriteLine(jsn);
            jsn = JsonSerializer.Serialize(Groups);
            sw.WriteLine(jsn);
        }
    }

    public void LoadAll()
    {
        if (File.Exists(_path))
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                string jsn = sr.ReadLine();
                Teachers = JsonSerializer.Deserialize<List<Teacher>>(jsn);
                jsn = sr.ReadLine();
                Students = JsonSerializer.Deserialize<List<Student>>(jsn);
                jsn = sr.ReadLine();
                Groups = JsonSerializer.Deserialize<List<Group>>(jsn);
            }
        }
    }

    public void SetPathForTests(string path)
    {
        _path = path;
    }
}