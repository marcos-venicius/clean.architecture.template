using Application.Common.Mappings;
using Domain.Entities;

namespace Application.CQRS.Todos.Queries.ListAllTodos;

public sealed class ListAllTodosQueryDto : IMapFrom<Todo>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Completed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}