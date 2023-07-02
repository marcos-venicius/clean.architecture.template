using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Events.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Todos.Commands.ChangeTodoState;

public sealed record ChangeTodoStateCommand(Guid Id, bool State) : IRequest;

public sealed class ChangeTodoStateCommandHandler : IRequestHandler<ChangeTodoStateCommand>
{
    private readonly IEfContext _context;

    public ChangeTodoStateCommandHandler(IEfContext context)
    {
        _context = context;
    }

    public async Task Handle(ChangeTodoStateCommand request, CancellationToken cancellationToken)
    {
        var todo = await _context
            .Todos
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (todo is null)
            throw new NotFoundException("this task does not exists");

        if (todo.Completed == request.State)
            return;

        todo.Completed = request.State;

        if (request.State)
            todo.AddDomainEvent(new CompleteTodoEvent(todo.Name));
        else
            todo.AddDomainEvent(new UncompleteTodoEvent(todo.Name));

        await _context.SaveChangesAsync(cancellationToken);
    }
}