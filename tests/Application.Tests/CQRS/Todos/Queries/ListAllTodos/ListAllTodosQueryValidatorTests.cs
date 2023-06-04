using Application.CQRS.Todos.Queries.ListAllTodos;

namespace Application.Tests.CQRS.Todos.Queries.ListAllTodos;

public sealed partial class ListAllTodosQueryValidatorTests
{
    [Fact]
    public void Should_Be_Invalid_When_Page_Is_Not_Valid()
    {
        var command = new ListAllTodosQuery(0, 1);

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Single();
        result.Errors[0].PropertyName.Should().Be("Page");
        result.Errors[0].ErrorMessage.Should().Be("invalid page");
    }

    [Fact]
    public void Should_Be_Invalid_When_PageSize_Is_Less_Than_1()
    {
        var command = new ListAllTodosQuery(1, 0);

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Single();
        result.Errors[0].PropertyName.Should().Be("PageSize");
        result.Errors[0].ErrorMessage.Should().Be("invalid page size");
    }

    [Fact]
    public void Should_Be_Invalid_When_PageSize_Is_Greater_Than_30()
    {
        var command = new ListAllTodosQuery(1, 31);

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Single();
        result.Errors[0].PropertyName.Should().Be("PageSize");
        result.Errors[0].ErrorMessage.Should().Be("max of 30 items per page");
    }

    [Fact]
    public void Should_Be_Valid_If_Everything_Is_Ok()
    {
        var command = new ListAllTodosQuery(1, 30);

        var sut = Sut();

        var result = sut.Validate(command);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}

#region Setup

public sealed partial class ListAllTodosQueryValidatorTests
{
    private ListAllTodosQueryValidator Sut()
    {
        return new();
    }
}

#endregion