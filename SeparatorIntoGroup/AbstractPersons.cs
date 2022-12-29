using SeparatorIntoGroup.Options;

namespace SeparatorIntoGroup;

public class AbstractPersons
{
    public long Id { get; protected set; }
    public string PersonName { get; protected set; }
    public string AccountName { get; protected set; }
    public StatusType Status { get; set; }
}