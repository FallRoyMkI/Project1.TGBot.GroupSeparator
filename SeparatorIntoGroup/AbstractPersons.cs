using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class AbstractPersons
{
    public int Id { get; protected set; }
    public string PersonName { get; protected set; }
    public string AccountName { get; protected set; }
    public StatusType Status { get; protected set; }

    public virtual void Writeinfo()
    {
        Console.WriteLine($"{Id}");
        Console.WriteLine($"{PersonName}");
        Console.WriteLine($"{AccountName}");
        Console.WriteLine($"{Status}");
    }
}