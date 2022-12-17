using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class TeamBuilder
{
    public List<Student> StudentsForDistribution { get; set; }
    private List<List<Student>> _teamList;
    private int[] _numberOfMembersInTeam;
    private int[,] _connectionValueForStudents;
    private int[] _connectionValueForTeams;
    private List<int> _randomStudentIndexes;

    
    public TeamBuilder(Group group, int[] numberOfTeamMembers) // тут массив вида 3/3/3/3/2/2/5 по участникам команд
    {
        StudentsForDistribution = group.StudentsInGroup.FindAll(x => x.Status == StatusType.PassedSurvey);
        _numberOfMembersInTeam = numberOfTeamMembers;
        for (int i = 0; i < _numberOfMembersInTeam.Length; i++)
        {
            _teamList.Add(new List<Student>());
        }

        _connectionValueForStudents = new int[StudentsForDistribution.Count, StudentsForDistribution.Count];
        //CreateConnections();
        _connectionValueForTeams = new int[_teamList.Count];

        CreationRandomStudentsIndexes();
    }
    //public void CreateConnections()
    //{

    //}
    public void CreationRandomStudentsIndexes()
    {
        Random random = new Random();
        for (int i = 0; i < StudentsForDistribution.Count; i++)
        {
            int k = random.Next(StudentsForDistribution.Count + 1);
            while (_randomStudentIndexes[i] != k )
            {
                if (!_randomStudentIndexes.Contains(k))
                {
                    _randomStudentIndexes[i] = k;
                }
                else
                {
                    k = random.Next(StudentsForDistribution.Count + 1);
                }
            }
        }
    }

    public void TeamBuild()
    {
        for (int i = 0; i < _teamList.Count; i++)
        {
            _teamList[i].Add(StudentsForDistribution[_randomStudentIndexes[i]]);  //добавляем капитаноф!)
        }

        _randomStudentIndexes = _randomStudentIndexes.GetRange(_teamList.Count, _randomStudentIndexes.Count - _teamList.Count);
        foreach (var studentIndex in _randomStudentIndexes)
        {
            Student student;
            foreach (var team in _teamList)
            {
                for (int i = 0; i < team.Count; i++)
                {
                    student = team[i];
                    _connectionValueForTeams[_teamList.FindIndex(x => x == team)] 
                        += _connectionValueForStudents[studentIndex, StudentsForDistribution.FindIndex(x => x == student)];
                }
                _teamList[GetIndexOfTeamForJoining(IndexMassiveCreation(_connectionValueForTeams))].Add(StudentsForDistribution[studentIndex]);
                _connectionValueForTeams = new int[_teamList.Count];
            }
        }
    }

    private int GetIndexOfTeamForJoining(int[] massive)
    {
        for (int i = 0; i < massive.Length; i++)
        {
            if (_teamList[i].Count < _numberOfMembersInTeam[i])
            {
                return i;
            }
        }
        return -1;
    }

    private int[] IndexMassiveCreation(int[] massive)
    {
        int[] indexMassive = new int[massive.Length];

        for (int i = 0; i < massive.Length; i++)
        {
            int maxValue = massive[i];
            int maxValueIndex = i;
            for (int j = i; j < massive.Length; j++)
            {
                if (massive[j] > maxValue)
                {
                    maxValue = massive[j];
                    maxValueIndex = j;
                    int temp = massive[i];
                    massive[i] = massive[j];
                    massive[j]= temp;
                }
            }
            indexMassive[i] = maxValueIndex;
        }

        return indexMassive;
    }
}