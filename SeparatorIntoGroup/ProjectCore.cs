using System.Text.Json;

namespace SeparatorIntoGroup;

public class ProjectCore
{
        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
        public List<Group> Groups { get; set; }
        public string Path { get; set; }

        public ProjectCore() 
        { 
            Teachers = new List<Teacher>();
            Students = new List<Student>();
            Groups = new List<Group>();
            Path = "../Storage.txt";
        }

        public void SaveAll()
        {
            using (StreamWriter sw = new StreamWriter(Path))
            {
                string jsn = JsonSerializer.Serialize(Teachers);
                sw.WriteLine(jsn);
                jsn = JsonSerializer.Serialize(Students);
                sw.WriteLine(jsn);
                jsn = JsonSerializer.Serialize(Groups);
                sw.WriteLine(jsn);
            }
        }
}