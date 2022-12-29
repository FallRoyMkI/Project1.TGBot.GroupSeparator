using System.Collections;
using SeparatorIntoGroup;

namespace SeparatorIntoGroupTests.TestCaseSource;

public class TeamTestsSources
{
    public class TableListTestsSources : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Student student = new Student(0, "Виктор", "@Vitya");
            yield return new Object[] { student };

            student = new Student(1, "Альберт", "@Albi");
            yield return new Object[] { student };
        }
    }
}