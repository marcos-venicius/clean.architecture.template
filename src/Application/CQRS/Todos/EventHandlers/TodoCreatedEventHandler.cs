using Domain.Events.Todos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Todos.EventHandlers;

public sealed class TodoCreatedEventHandler : INotificationHandler<TodoCreatedEvent>
{
    private readonly ILogger<TodoCreatedEvent> _logger;

    public TodoCreatedEventHandler(ILogger<TodoCreatedEvent> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            @"NEW TODO CREATED ""{name}""",
            notification.Name
        );

        return Task.CompletedTask;
    }
}