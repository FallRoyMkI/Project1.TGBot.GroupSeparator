using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class TeamBuilder
{
    public List<List<Student>> TeamList { get; private set; }
    private List<Student> StudentsForDistribution;
    private List<int> _numberOfMembersForEachTeam;
    private int[,] _connectionValueForStudents;
    private int[] _connectionValueForTeams;
    private List<int> _randomStudentIndexes = new List<int>();
    private IEnumerable<TimeDictionaryKeys> _timeDictionaryKeys = new[]
    {
        TimeDictionaryKeys.Понедельник,
        TimeDictionaryKeys.Вторник,
        TimeDictionaryKeys.Среда,
        TimeDictionaryKeys.Четверг,
        TimeDictionaryKeys.Пятница,
        TimeDictionaryKeys.Суббота,
        TimeDictionaryKeys.Воскресенье,
    };

    public TeamBuilder(List<Student> distribution, List<int> numberOfMembers)
    {
        TeamList = new List<List<Student>>();

        StudentsForDistribution = distribution;
        _numberOfMembersForEachTeam = numberOfMembers;

        for (int i = 0; i < _numberOfMembersForEachTeam.Count; i++)
        {
            TeamList.Add(new List<Student>());
        }

        _connectionValueForStudents = new int[StudentsForDistribution.Count, StudentsForDistribution.Count];
        CreateConnections();
        _connectionValueForTeams = new int[TeamList.Count];

        CreationRandomStudentsIndexes();
    }


    public void TeamBuild()
    {
        for (int i = 0; i < TeamList.Count; i++)
        {
            TeamList[i].Add(StudentsForDistribution[_randomStudentIndexes[i]]); 
        }

        _randomStudentIndexes = _randomStudentIndexes.GetRange(TeamList.Count, _randomStudentIndexes.Count - TeamList.Count);
        foreach (var studentIndex in _randomStudentIndexes)
        {
            Student student;
            foreach (var team in TeamList)
            {
                for (int i = 0; i < team.Count; i++)
                {
                    student = team[i];
                    _connectionValueForTeams[TeamList.FindIndex(x => x == team)]
                        += _connectionValueForStudents[studentIndex, StudentsForDistribution.FindIndex(x => x == student)];
                    _connectionValueForTeams[TeamList.FindIndex(x => x == team)] /= team.Count;
                }
            }
            TeamList[GetIndexOfTeamForJoining(IndexArrayCreation(_connectionValueForTeams))].Add(StudentsForDistribution[studentIndex]);
            _connectionValueForTeams = new int[TeamList.Count];
        }
    }

    private void CreateConnections()
    {
        int value;
        for (int i = 0; i < StudentsForDistribution.Count; i++)
        {
            for (int j = 0; j < StudentsForDistribution.Count; j++)
            {
                value = 0;
                if (j == i)
                {
                    _connectionValueForStudents[i, i] = -1000;
                }
                else
                {
                    value += WishListsComparison(StudentsForDistribution[i], StudentsForDistribution[j]);
                    value += TimeComparison(StudentsForDistribution[i], StudentsForDistribution[j]);

                    _connectionValueForStudents[i, j] = value;
                }
            }
        }
    }
    private void CreationRandomStudentsIndexes()
    {
        Random random = new Random();
        while (_randomStudentIndexes.Count != StudentsForDistribution.Count)
        {
            int randomIndex = random.Next(StudentsForDistribution.Count);
            if (!_randomStudentIndexes.Contains(randomIndex))
            {
                _randomStudentIndexes.Add(randomIndex);
            }
        }
    }

    private int GetIndexOfTeamForJoining(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (TeamList[i].Count < _numberOfMembersForEachTeam[i])
            {
                return i;
            }
        }
        return -1;
    }
    private int[] IndexArrayCreation(int[] array)
    {
        int[] indexArray = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            int maxValue = array[i];
            int maxValueIndex = i;
            for (int j = i; j < array.Length; j++)
            {
                if (array[j] > maxValue)
                {
                    maxValue = array[j];
                    maxValueIndex = j;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            indexArray[i] = maxValueIndex;
        }

        return indexArray;
    }

    private int WishListsComparison(Student firstStudent, Student secondStudent)
    {
        if (firstStudent.AnswersToQuestionnaire.PreferredTeammates.Contains(secondStudent.AccountName)
            && secondStudent.AnswersToQuestionnaire.PreferredTeammates.Contains(firstStudent.AccountName)) return 50;
        if (firstStudent.AnswersToQuestionnaire.NotPreferredTeammates.Contains(secondStudent.AccountName)
            && secondStudent.AnswersToQuestionnaire.NotPreferredTeammates.Contains(firstStudent.AccountName)) return -50;

        if (!firstStudent.AnswersToQuestionnaire.PreferredTeammates.Contains(secondStudent.AccountName)
            && !firstStudent.AnswersToQuestionnaire.NotPreferredTeammates.Contains(secondStudent.AccountName))
        {
            if (secondStudent.AnswersToQuestionnaire.PreferredTeammates.Contains(firstStudent.AccountName)) return 25;
            if (secondStudent.AnswersToQuestionnaire.NotPreferredTeammates.Contains(firstStudent.AccountName)) return -25;
        }

        if (!secondStudent.AnswersToQuestionnaire.PreferredTeammates.Contains(firstStudent.AccountName)
            && !secondStudent.AnswersToQuestionnaire.NotPreferredTeammates.Contains(firstStudent.AccountName))
        {
            if (firstStudent.AnswersToQuestionnaire.PreferredTeammates.Contains(secondStudent.AccountName)) return 25;
            if (firstStudent.AnswersToQuestionnaire.NotPreferredTeammates.Contains(secondStudent.AccountName)) return -25;
        }

        return 0;
    }
    private int TimeComparison(Student firstStudent, Student secondStudent)
    {
        int value = 0;
        foreach (var key in _timeDictionaryKeys)
        {
            foreach (var freeTimeNode in firstStudent.AnswersToQuestionnaire.StudentFreeTime[key])
            {
                if (secondStudent.AnswersToQuestionnaire.StudentFreeTime[key].Contains(freeTimeNode))
                {
                    value += 10;
                }
            }
        }

        return value;
    }
}