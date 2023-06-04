using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Todos;
using MediatR;

namespace Application.CQRS.Todos.Commands.CreateTodo;

public sealed record CreateTodoCommand(string Name) : IRequest<Guid>;

public sealed class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Guid>
{
    private readonly IEFContext _context;

    public CreateTodoCommandHandler(IEFContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Name = request.Name
        };

        todo.AddDomainEvent(new TodoCreatedEvent(request.Name));

        _context.Todos.Add(todo);

        await _context.SaveChangesAsync(cancellationToken);

        return todo.Id;
    }
}