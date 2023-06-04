using Application.CQRS.Todos.Commands.CreateTodo;

namespace Application.Tests.CQRS.Todos.Commands.CreateTodo;

public sealed partial class CreateTodoCommandValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Should_Be_Invalid_When_Name_Is_Empty(string name)
    {
        var command = new CreateTodoCommand(name);

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Single();
        result.Errors[0].PropertyName.Should().Be("Name");
        result.Errors[0].ErrorMessage.Should().Be("required field");
    }

    [Fact]
    public void Should_Be_Invalid_When_Name_Short()
    {
        var command = new CreateTodoCommand("a");

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Single();
        result.Errors[0].PropertyName.Should().Be("Name");
        result.Errors[0].ErrorMessage.Should().Be("min length of 3");
    }

    [Fact]
    public void Should_Be_Invalid_When_Name_Long()
    {
        var command = new CreateTodoCommand(new string('a', 251));

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Single();
        result.Errors[0].PropertyName.Should().Be("Name");
        result.Errors[0].ErrorMessage.Should().Be("max length of 250");
    }

    [Fact]
    public void Should_Be_Valid_When_All_Is_Fine()
    {
        var command = new CreateTodoCommand("name");

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}

#region Setup

public sealed partial class CreateTodoCommandValidatorTests
{
    private CreateTodoCommandValidator Sut()
    {
        return new CreateTodoCommandValidator();
    }
}

#endregion