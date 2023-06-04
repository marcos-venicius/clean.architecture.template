using Domain.Common;

namespace Domain.Events.Todos;

public sealed class CompleteTodoEvent : BaseEvent
{
    public string Name { get; }

    public CompleteTodoEvent(string name)
    {
        Name = name;
    }
}