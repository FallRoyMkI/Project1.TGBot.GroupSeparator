using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class AbstractPersons
{
    public int Id { get; protected set; }
    public string PersonName { get; protected set; }
    public string AccountName { get; protected set; }
    public StatusType Status { get; set; }

    public virtual void WriteInfo()
    {
        Console.WriteLine($"{Id}");
        Console.WriteLine($"{PersonName}");
        Console.WriteLine($"{AccountName}");
        Console.WriteLine($"{Status}");
    }

    //public override bool Equals(object? obj)
    //{
    //    return obj is AbstractPersons ap &&
    //           Id == ap.Id;
    //}
}