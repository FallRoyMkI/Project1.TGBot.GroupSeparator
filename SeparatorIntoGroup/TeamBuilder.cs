using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class TeamBuilder
{
    public List<Student> StudentsForDistribution { get; set; }
    public List<List<Student>> TeamList { get; private set; }
    private int[] _numberOfMembersInTeam;
    private int[,] _connectionValueForStudents;
    private int[] _connectionValueForTeams;
    private List<int> _randomStudentIndexes;
    private IEnumerable<TimeDictionaryKeys> _timeDictionaryKeys;

    public TeamBuilder(Group group, int[] numberOfTeamMembers) // тут массив вида 3/3/3/3/2/2/5 по участникам команд
    {
        StudentsForDistribution = group.StudentsInGroup.FindAll(x => x.Status == StatusType.PassedSurvey);
        _numberOfMembersInTeam = numberOfTeamMembers;
        for (int i = 0; i < _numberOfMembersInTeam.Length; i++)
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
            TeamList[i].Add(StudentsForDistribution[_randomStudentIndexes[i]]);  //добавляем капитаноф!)
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
                }
                TeamList[GetIndexOfTeamForJoining(IndexArrayCreation(_connectionValueForTeams))].Add(StudentsForDistribution[studentIndex]);
                _connectionValueForTeams = new int[TeamList.Count];
            }
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
        for (int i = 0; i < StudentsForDistribution.Count; i++)
        {
            int randomIndex = random.Next(StudentsForDistribution.Count + 1);
            while (_randomStudentIndexes[i] != randomIndex)
            {
                if (!_randomStudentIndexes.Contains(randomIndex))
                {
                    _randomStudentIndexes[i] = randomIndex;
                }
                else
                {
                    randomIndex = random.Next(StudentsForDistribution.Count + 1);
                }
            }
        }
    }

    private int GetIndexOfTeamForJoining(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (TeamList[i].Count < _numberOfMembersInTeam[i])
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
        if (firstStudent.AnswersToQuestionnaire.WishStudents.Contains(secondStudent) && secondStudent.AnswersToQuestionnaire.WishStudents.Contains(firstStudent)) return 50;
        if (firstStudent.AnswersToQuestionnaire.NotWishStudents.Contains(secondStudent) && secondStudent.AnswersToQuestionnaire.NotWishStudents.Contains(firstStudent)) return -50;

        if (!firstStudent.AnswersToQuestionnaire.WishStudents.Contains(secondStudent) && !firstStudent.AnswersToQuestionnaire.NotWishStudents.Contains(secondStudent))
        {
            if (secondStudent.AnswersToQuestionnaire.WishStudents.Contains(firstStudent)) return 25;
            if (secondStudent.AnswersToQuestionnaire.NotWishStudents.Contains(firstStudent)) return -25;
        }

        if (!secondStudent.AnswersToQuestionnaire.WishStudents.Contains(firstStudent) && !secondStudent.AnswersToQuestionnaire.NotWishStudents.Contains(firstStudent))
        {
            if (firstStudent.AnswersToQuestionnaire.WishStudents.Contains(secondStudent)) return 25;
            if (firstStudent.AnswersToQuestionnaire.NotWishStudents.Contains(secondStudent)) return -25;
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