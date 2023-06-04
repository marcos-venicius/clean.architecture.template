using Application.Common.Interfaces;
using Domain.Events.Todos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Todos.Commands.CompleteTodo;

public sealed record ChangeTodoStateCommand(Guid Id, bool state) : IRequest;

public sealed class ChangeTodoStateCommandHandler : IRequestHandler<ChangeTodoStateCommand>
{
    private readonly IEFContext _context;

    public ChangeTodoStateCommandHandler(IEFContext context)
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

        if (todo.Completed == request.state)
            return;

        todo.Completed = request.state;

        if (request.state)
            todo.AddDomainEvent(new CompleteTodoEvent(todo.Name));
        else
            todo.AddDomainEvent(new UncompleteTodoEvent(todo.Name));

        await _context.SaveChangesAsync(cancellationToken);
    }
}