using System.Collections;
using System.IO;
using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup.Tests.TestCaseSources;

public class TeamTestsSources : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        Student student = new Student(3, "Виктор", "@vitya");
        yield return new Object[] { student};

        student = new Student(4, "Альберт", "@albi");
        yield return new Object[] { student };

        student = new Student(1, "Рузвальд", "@ruzz");
        yield return new Object[] { student };
    }
}