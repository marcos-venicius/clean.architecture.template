using Domain.Common;

namespace Domain.Events.Todos;

public sealed class TodoCreatedEvent : BaseEvent
{
    public string Name { get; } = string.Empty;

    public TodoCreatedEvent(string name)
    {
        Name = name;
    }
}