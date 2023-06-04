using FluentValidation;

namespace Application.CQRS.Todos.Commands.CreateTodo;

public sealed class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("required field");

        When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
        {
            RuleFor(x => x.Name.Length)
                .Configure(x => x.PropertyName = "Name")
                .GreaterThanOrEqualTo(3)
                .WithMessage("min length of 3")
                .LessThanOrEqualTo(250)
                .WithMessage("max length of 250");
        });
    }
}