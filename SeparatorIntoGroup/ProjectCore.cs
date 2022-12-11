namespace SeparatorIntoGroup;
using System.Text.Json;


public class ProjectCore
{
        public List<Student> Students { get; set; }
        public List<Group> Groups { get; set; }
        public List<Teacher> Teachers { get; set; }
        public string Path { get; set; }

        public ProjectCore() 
        { 
            Students = new List<Student>();
            Groups = new List<Group>();
            Teachers = new List<Teacher>();
            Path = "../Storage.txt";
        }

        public void SaveAll()
        {
            using (StreamWriter sw = new StreamWriter(Path))
            {
                string jsn = JsonSerializer.Serialize(Students);
                sw.WriteLine(jsn);
                jsn = JsonSerializer.Serialize(Groups);
                sw.WriteLine(jsn);
                jsn = JsonSerializer.Serialize(Teachers);
                sw.WriteLine(jsn);
            }
        }
}