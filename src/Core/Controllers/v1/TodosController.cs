using Application.Common.Models;
using Application.CQRS.Todos.Commands.CompleteTodo;
using Application.CQRS.Todos.Commands.CreateTodo;
using Application.CQRS.Todos.Queries.ListAllTodos;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers.v1;

[Route("/api/v{version:apiVersion}/todos")]
[ApiVersion("1.0")]
public sealed class TodosController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create(CreateTodoCommand command)
    {
        var result = await Mediator.Send(command);

        return new JsonResult(result)
        {
            StatusCode = 201
        };
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedList<ListAllTodosQueryDto>>> List(uint? page, uint? pageSize)
    {
        var command = new ListAllTodosQuery(page ?? 1, pageSize ?? 10);

        var result = await Mediator.Send(command);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/complete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Complete(Guid id)
    {
        var command = new ChangeTodoStateCommand(id, true);

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("{id:guid}/uncomplete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Uncomplete(Guid id)
    {
        var command = new ChangeTodoStateCommand(id, false);

        await Mediator.Send(command);

        return NoContent();
    }
}