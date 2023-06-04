using Application.CQRS.Todos.Commands.CreateTodo;
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
}