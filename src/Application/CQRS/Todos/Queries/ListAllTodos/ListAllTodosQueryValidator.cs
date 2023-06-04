using FluentValidation;

namespace Application.CQRS.Todos.Queries.ListAllTodos;

public sealed class ListAllTodosQueryValidator : AbstractValidator<ListAllTodosQuery>
{
    public ListAllTodosQueryValidator()
    {
        RuleFor(x => (int)x.Page)
            .GreaterThanOrEqualTo(1)
            .WithMessage("invalid page");

        RuleFor(x => (int)x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("invalid page size")
            .LessThanOrEqualTo(30)
            .WithMessage("max of 30 items per page");
    }
}