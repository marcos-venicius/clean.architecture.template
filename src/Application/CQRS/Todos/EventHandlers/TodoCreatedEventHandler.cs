using Domain.Events.Todos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Todos.EventHandlers;

public sealed class TodoCreatedEventHandler : INotificationHandler<TodoCreatedEvent>
{
    private readonly ILogger<TodoCreatedEventHandler> _logger;

    public TodoCreatedEventHandler(ILogger<TodoCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            @"NEW TODO CREATED ""{Name}""",
            notification.Name
        );

        return Task.CompletedTask;
    }
}