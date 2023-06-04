using Application.Common.Interfaces;
using Application.CQRS.Todos.Commands.CreateTodo;
using Domain.Entities;

namespace Application.Tests.CQRS.Todos.Commands.CreateTodo;

public sealed partial class CreateTodoCommandHandlerTests
{
    [Fact]
    public async Task Should_Create_A_New_Todo()
    {
        var todos = new List<Todo>().GenerateMockDbSet();

        _contextMock
            .Setup(x => x.Todos)
            .Returns(todos.Object);

        var command = new CreateTodoCommand("test");

        var sut = Sut();

        await sut.Handle(command, CancellationToken.None);

        todos.Verify(x => x.Add(It.IsAny<Todo>()), Times.Once);
        _contextMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}

public sealed partial class CreateTodoCommandHandlerTests
{
    private readonly Mock<IEFContext> _contextMock;

    public CreateTodoCommandHandlerTests()
    {
        _contextMock = new Mock<IEFContext>();
    }

    private CreateTodoCommandHandler Sut()
    {
        return new(_contextMock.Object);
    }
}