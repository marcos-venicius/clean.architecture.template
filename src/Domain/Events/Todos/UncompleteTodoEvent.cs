using Domain.Common;

namespace Domain.Events.Todos;

public sealed class UncompleteTodoEvent : BaseEvent
{
    public string Name { get; }

    public UncompleteTodoEvent(string name)
    {
        Name = name;
    }
}