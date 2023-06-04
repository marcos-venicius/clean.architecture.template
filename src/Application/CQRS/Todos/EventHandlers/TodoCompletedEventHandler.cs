using Domain.Events.Todos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Todos.EventHandlers;

public sealed class TodoCompletedEventHandler : INotificationHandler<CompleteTodoEvent>
{
    private readonly ILogger<TodoCompletedEventHandler> _logger;

    public TodoCompletedEventHandler(ILogger<TodoCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CompleteTodoEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            @"TODO ""{name}"" COMPLETED SUCCESSFULLY",
            notification.Name
        );

        return Task.CompletedTask;
    }
}