using Domain.Events.Todos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Todos.EventHandlers;

public sealed class TodoUncompletedEventHandler : INotificationHandler<UncompleteTodoEvent>
{
    private readonly ILogger<TodoUncompletedEventHandler> _logger;

    public TodoUncompletedEventHandler(ILogger<TodoUncompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UncompleteTodoEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            @"TODO ""{name}"" UNCOMPLETED SUCCESSFULLY",
            notification.Name
        );

        return Task.CompletedTask;
    }
}